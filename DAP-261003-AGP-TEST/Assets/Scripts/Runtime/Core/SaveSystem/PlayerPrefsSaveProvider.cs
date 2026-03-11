using DAP.Runtime.Core;
using UnityEngine;

namespace DAP.Runtime.Core
{
    public class PlayerPrefsSaveProvider : ISaveProvider
    {
        public void SaveStars(int levelIndex, int stars)
        {
            string key = $"Level_{levelIndex}_Stars";
            if (stars > PlayerPrefs.GetInt(key, 0)) PlayerPrefs.SetInt(key, stars);
        }

        public int GetStars(int levelIndex) => PlayerPrefs.GetInt($"Level_{levelIndex}_Stars", 0);
        
        public void Load()
        {
            //
        }
    }
}