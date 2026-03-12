using DAP.Runtime.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAP.Runtime.Core
{
    public class AudioManager : MonoBehaviour
    {
         [SerializeField] private AudioLibrarySO _library; 

        private AudioSource _sfxSource;
        private static AudioManager _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            _sfxSource = GetComponent<AudioSource>();
        }

        private void OnEnable() => AudioDispatcher.OnPlaySFX += PlaySound;
        private void OnDisable() => AudioDispatcher.OnPlaySFX -= PlaySound;

        private void PlaySound(SFXType type)
        {
            AudioClip clip = _library.GetClip(type);
            if (clip != null) 
                _sfxSource.PlayOneShot(clip);
        }
    }
}