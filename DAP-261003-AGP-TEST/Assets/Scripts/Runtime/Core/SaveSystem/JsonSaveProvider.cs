using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DAP.Runtime.Core
{
    public class JsonSaveProvider : ISaveProvider
    {
        private string _savePath => Path.Combine(Application.persistentDataPath, MainConfig.SaveProvider.SAVE_JSON_NAME);

        private GameData _cachedData;

        [System.Serializable]
        public class GameData
        {
            public List<int> levelStars = new();
        }

        public void Load()
        {
            if (!File.Exists(_savePath))
            {
                _cachedData = new GameData();
                return;
            }

            try
            {
                string json = File.ReadAllText(_savePath);
                _cachedData = JsonUtility.FromJson<GameData>(json);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[JsonSaveProvider] Failed to load save data: {e.Message}");
                _cachedData = new GameData();
            }
        }

        public int GetStars(int levelIndex)
        {
            if (_cachedData == null) Load();

            if (levelIndex < 0 || levelIndex >= _cachedData.levelStars.Count) return 0;
            return _cachedData.levelStars[levelIndex];
        }

        public void SaveStars(int levelIndex, int stars)
        {
            if (_cachedData == null) Load();

            while (_cachedData.levelStars.Count <= levelIndex)
                _cachedData.levelStars.Add(0);

            if (stars > _cachedData.levelStars[levelIndex])
            {
                _cachedData.levelStars[levelIndex] = stars;
                SaveToFile();
            }
        }

        private void SaveToFile()
        {
            try
            {
                string json = JsonUtility.ToJson(_cachedData);
                File.WriteAllText(_savePath, json);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[JsonSaveProvider] Failed to save to disk: {e.Message}");
            }
        }
    }
}