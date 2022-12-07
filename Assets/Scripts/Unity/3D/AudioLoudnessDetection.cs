using System;
using UnityEngine;

namespace Unity._3D
{
    public class AudioLoudnessDetection : MonoBehaviour
    {
        public AudioSource micSource;
        public int sampleWindow = 64;
        public float threshold = 0.005f;
        public int interruptibleDuration = 1000;

        public int testPosition;
        public float testLoudness;
        public bool testIsSpeaking;
        public long testTimestamp;

        private long _prevSpeakingTimestamp;

        public bool IsSpeaking()
        {
            var currentTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

            var clip = micSource.clip;
            var clipPosition = Microphone.GetPosition(null);

            var startPosition = clipPosition - sampleWindow;
            if (startPosition < 0) return false;

            var waveData = new float[sampleWindow];
            clip.GetData(waveData, startPosition);

            //compute loudness
            float totalLoudness = 0;
            for (var i = 0; i < sampleWindow; i++) totalLoudness += Mathf.Abs(waveData[i]);

            var isSpeakingAtTheMoment = totalLoudness / sampleWindow > threshold;
            if (isSpeakingAtTheMoment) _prevSpeakingTimestamp = currentTimestamp;

            var isSpeaking = currentTimestamp - _prevSpeakingTimestamp <= interruptibleDuration;

            // ***************** test
            testLoudness = totalLoudness / sampleWindow;
            testPosition = clipPosition;
            testIsSpeaking = isSpeaking;
            testTimestamp = currentTimestamp;
            // *********************

            return isSpeaking;
        }
    }
}