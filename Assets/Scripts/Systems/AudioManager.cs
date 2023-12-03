using Colorado.Core;
using UnityEngine;

namespace Colorado.Systems
{
    public sealed class AudioManager : BaseSingeltonSystem<AudioManager>
    {
        [SerializeField] private AudioClip[] _fxs;

        private AudioSource _audio;
        private AudioSource _backgroundAudio;

        public bool IsMute
        {
            get => IsMute;
            set
            {
                IsMute = value;
                _audio.mute = (IsMute) ? true : false;
                _backgroundAudio.mute = (IsMute) ? true : false;
            }
        }

        public override void StartGame()
        {
            _audio = GetComponent<AudioSource>();
            _backgroundAudio = GetComponentInChildren<AudioSource>();
        }

        public void PlaySound(int id)
        {
            if (_audio == null)
                return;

            _audio.PlayOneShot(_fxs[id]);
        }
    }
}
