using DAP.Runtime.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAP.Runtime.UI
{
    public class LevelSelectionManager : MonoBehaviour
    {
        [SerializeField] private LevelLibraryDataSO _library;
        [SerializeField] private LevelButton _buttonPrefab;
        [SerializeField] private Transform _container;

        private void Start()
        {
            GenerateButtons();
        }

        private void GenerateButtons()
        {
            //// Bersihkan container dulu
            //foreach (Transform child in _container) Destroy(child.gameObject);

            //var levelData = _library.GetLevelDataSO();

            //for (int i = 0; i < levelData.Count; i++)
            //{
            //    var curLevelData = levelData[i];

            //    // LOGIC UNLOCK: Level 1 (index 0) selalu kebuka, 
            //    // sisanya cek apakah level sebelumnya dapet minimal 1 bintang
            //    bool isLocked = false;
            //    if (i > 0)
            //    {
            //        int prevLevelStars = SaveSystem.GetStars(i - 1);
            //        isLocked = prevLevelStars <= 0;
            //    }

            //    int currentStars = SaveSystem.GetStars(i);

            //    var btn = Instantiate(_buttonPrefab, _container);
            //    btn.Setup(curLevelData, isLocked, currentStars, StartLevel);
            //}
        }

        private void StartLevel(LevelDataSO data)
        {
            // Kirim data ke GameManager (bisa lewat Scene transition atau Singleton)
            //GameManager.Instance.LoadLevel(data);
        }
    }
}