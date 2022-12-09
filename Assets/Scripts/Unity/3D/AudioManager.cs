using System;
using UnityEngine;
using Xiangqi.Enum;

namespace Unity._3D
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource audioSource;

        public AudioClip chessPieceSmashSound;
        public AudioClip redTurn;
        public AudioClip blackTurn;
        public AudioClip check;
        public AudioClip congratRed;
        public AudioClip congratBlack;
        public AudioClip welcome;


        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null) throw new Exception("Cannot find audio source component");
        }

        public void PlaySound(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }

        public void PlayChessPieceSmashSound()
        {
            PlaySound(chessPieceSmashSound);
        }

        public void PlayTurnSound(string side)
        {
            if (side == Side.Black)
                PlaySound(blackTurn);
            else
                PlaySound(redTurn);
        }

        public void PlayCongratSound(string side)
        {
            if (side == Side.Black)
                PlaySound(congratBlack);
            else
                PlaySound(congratRed);
        }

        public void PlayCheckSound()
        {
            PlaySound(check);
        }
    }
}