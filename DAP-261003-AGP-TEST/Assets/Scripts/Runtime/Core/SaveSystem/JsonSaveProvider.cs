using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DAP.Runtime.Core
{
    public class JsonSaveProvider : ISaveProvider
    {
        private string SavePath => Path.Combine(Application.persistentDataPath, MainConfig.SaveProvider.SAVE_JSON_NAME);

        private GameData _cachedData;

        [System.Serializable]
        public class GameData
        {
            public List<int> LevelStars = new();
        }

        public void Load()
        {
            if (!File.Exists(SavePath))
            {
                _cachedData = new GameData();
                return;
            }

            try
            {
                string json = File.ReadAllText(SavePath);
                _cachedData = JsonUtility.FromJson<GameData>(json);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to load save data: {e.Message}");
                _cachedData = new GameData();
            }
        }

        public int GetStars(int levelIndex)
        {
            if (_cachedData == null) Load();

            if (levelIndex < 0 || levelIndex >= _cachedData.LevelStars.Count) return 0;
            return _cachedData.LevelStars[levelIndex];
        }

        public void SaveStars(int levelIndex, int stars)
        {
            if (_cachedData == null) Load();

            while (_cachedData.LevelStars.Count <= levelIndex)
                _cachedData.LevelStars.Add(0);

            if (stars > _cachedData.LevelStars[levelIndex])
            {
                _cachedData.LevelStars[levelIndex] = stars;
                SaveToFile();
            }
        }

        private void SaveToFile()
        {
            try
            {
                string json = JsonUtility.ToJson(_cachedData);
                File.WriteAllText(SavePath, json);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to save to disk: {e.Message}");
            }
        }
    }
}