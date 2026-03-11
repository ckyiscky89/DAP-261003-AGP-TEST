using DAP.Runtime.Core;
using DAP.Runtime.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DAP.Runtime.UI
{
    public class LevelSelectionManager : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private LevelLibraryDataSO _library;

        [Header("References")]
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private GridLayoutGroup _gridLayoutLevelSelection;

        private ISaveProvider _saveProvider;
        private bool _isInitialized = false;

        public void Initialize()
        {
            if (!_isInitialized)
            {
                InitializeSaveProvider();
                _isInitialized = true;
            }

            GenerateButtons();
        }

        private void InitializeSaveProvider()
        {
            _saveProvider = new JsonSaveProvider();
            _saveProvider.Load();
        }

        private void GenerateButtons()
        {
            ClearLevelGrid();

            var curLevel = _library.GetLevelDataSO();
            for (int i = 0; i < curLevel.Count; i++)
            {
                LevelDataSO level = curLevel[i];

                bool isLocked = false;
                if (i > 0)
                {
                    int prevStars = _saveProvider.GetStars(i - 1);
                    isLocked = (prevStars <= 0);
                }

                int currentStars = _saveProvider.GetStars(i);

                var gridLayout = _gridLayoutLevelSelection.transform;
                LevelButton btn = Instantiate(_levelButtonPrefab, gridLayout);
                btn.Setup(level, isLocked, currentStars, OnLevelClicked);
            }
        }

        private void ClearLevelGrid()
        {
            var gridLayout = _gridLayoutLevelSelection.transform;
            foreach (Transform child in gridLayout)
                Destroy(child.gameObject);
        }

        private void OnLevelClicked(LevelDataSO level)
        {
            SessionState.selectedLevelData = level;
            SceneManager.LoadScene(MainConfig.SceneName.SCENE_GAMEPLAY);
        }
    }
}