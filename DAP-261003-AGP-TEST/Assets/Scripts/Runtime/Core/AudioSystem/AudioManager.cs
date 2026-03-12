using DAP.Runtime.Data;
using UnityEngine;

namespace DAP.Runtime.Core
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioLibrarySO _library;
        [SerializeField] private AudioSource _sourceSFX;

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
        }

        private void OnEnable() => AudioDispatcher.onPlaySFX += PlaySound;
        private void OnDisable() => AudioDispatcher.onPlaySFX -= PlaySound;

        private void PlaySound(SFXType type)
        {
            AudioClip clip = _library.GetClip(type);

            if (clip != null)
                _sourceSFX?.PlayOneShot(clip);
        }
    }
}