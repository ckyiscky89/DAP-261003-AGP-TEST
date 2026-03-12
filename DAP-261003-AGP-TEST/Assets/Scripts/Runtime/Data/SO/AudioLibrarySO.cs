using UnityEngine;

namespace DAP.Runtime.Data
{
    public enum SFXType { ButtonClick, CardFlip, Match, Mismatch, Win, Lose }

    [CreateAssetMenu(fileName = "AudioLibraryData", menuName = "DAP/Data/New Audio Library Data")]
    public class AudioLibrarySO : ScriptableObject
    {
        [Header("UI Sounds")]
        public AudioClip buttonClick;

        [Header("Gameplay SFX")]
        public AudioClip cardFlip;
        public AudioClip match;
        public AudioClip mismatch;
        public AudioClip win;
        public AudioClip lose;

        public AudioClip GetClip(SFXType type)
        {
            return type switch
            {
                SFXType.ButtonClick => buttonClick,
                SFXType.CardFlip => cardFlip,
                SFXType.Match => match,
                SFXType.Mismatch => mismatch,
                SFXType.Win => win,
                SFXType.Lose => lose,
                _ => null
            };
        }
    }
}