using System;
using System.Linq;
using API.Speech_AI;
using Libs;
using UnityEngine;

namespace Unity._3D
{
    public class MicManager : MonoBehaviour
    {
        private const int AudioFrequency = 48000;

        public AudioSource micSource;
        public AudioLoudnessDetection loudnessDetector;

        private bool _isSpeaking;
        private int _speakingEndPos;
        private int _speakingStartPos;
        private DateTime _speakingStartTime;

        private void Start()
        {
            micSource.clip = Microphone.Start(null, true, 300, AudioFrequency);

            // while (!(Microphone.GetPosition(null) > 0))
            // {
            // }
            //
            // micSource.Play();
        }

        private void FixedUpdate()
        {
            var newSpeaking = loudnessDetector.IsSpeaking();
            if (newSpeaking == _isSpeaking) return;

            _isSpeaking = newSpeaking;
            var clipPos = Microphone.GetPosition(null);

            if (newSpeaking)
            {
                print("Started speaking");

                var micClip = micSource.clip;
                var rollback = (int)(micClip.frequency * micClip.channels * 0.3f); // 300ms of samples
                var totalSamples = micClip.samples * micClip.channels;
                _speakingStartPos =
                    (clipPos - rollback + totalSamples) % totalSamples; // wrap around if negative position
                _speakingStartTime = DateTime.UtcNow;
            }
            else
            {
                _speakingEndPos = clipPos;

                var timeElapsed = (DateTime.UtcNow - _speakingStartTime).TotalMilliseconds;
                if (timeElapsed < 1000)
                {
                    print("Stopped speaking, but duration too short!");
                }
                else
                {
                    print("Stopped speaking. Sending audio to server...");
                    GetSpeakingClip();
                }
            }
        }

        private void GetSpeakingClip()
        {
            var micClip = micSource.clip;

            var totalSamples = micClip.samples * micClip.channels;
            var currentClipData = new float[totalSamples];

            var getClipData = micSource.clip.GetData(currentClipData, 0);
            if (!getClipData) return;

            float[] voiceData;

            if (_speakingStartPos <= _speakingEndPos)
                voiceData = currentClipData[_speakingStartPos.._speakingEndPos];

            else
                voiceData = currentClipData[_speakingEndPos..].Concat(currentClipData[..(_speakingStartPos + 1)])
                    .ToArray();

            var voiceClip =
                AudioClip.Create("sendBuffer", voiceData.Length, micClip.channels, micClip.frequency, false);
            voiceClip.SetData(voiceData, 0);

            StartCoroutine(SpeechService.UploadSpeech(SavWav.GetWav(voiceClip, out _, true)));
            // SavWav.Save($"myclip_{DateTime.UtcNow.ToFileTime()}", voiceClip, true);
        }
    }
}