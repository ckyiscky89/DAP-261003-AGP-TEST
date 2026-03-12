using DAP.Runtime.Core;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DAP.Runtime.UI
{
    public class GameUIManager : MonoBehaviour
    {
        [Header("Core Reference")]
        [SerializeField] private GameplayManager _gameplayManager;

        [Header("UI Reference")]
        [SerializeField] private TMP_Text _txtMisses;
        [SerializeField] private TMP_Text _txtTimer, _txtCombo;

        [Header("Result Reference")]
        [SerializeField] private GameObject _containerResult;
        [SerializeField] private List<GameObject> _listStars;
        [SerializeField] private TMP_Text _txtResultTitle, _txtFinalTime, _txtMaxCombo;
        [SerializeField] private Button _btnRetry, _btnMenu, _btnNext;

        private void OnEnable()
        {
            if (!IsGameplayManagerValid()) return;

            SetGameplayManager();
            SetResultButton();
        }

        private void OnDisable()
        {
            if (!IsGameplayManagerValid()) return;

            ResetGameplayManager();
            ResetResultButton();
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitializeResultScreen();
            InitializeVisual();
        }

        private void InitializeResultScreen()
        {
            _containerResult.SetActive(false);
            _txtFinalTime.gameObject.SetActive(false);
            _txtMaxCombo.gameObject.SetActive(false);
        }

        private void InitializeVisual()
        {
            UpdateTimerVisual(0);
            UpdateComboVisual(0);
            UpdateMissesVisual(0);
        }

        private void SetResultButton()
        {
            AddButtonListener(_btnRetry, OnRetryClicked);
            AddButtonListener(_btnMenu, OnMenuClicked);
            AddButtonListener(_btnNext, OnNextClicked);
        }

        private void SetGameplayManager()
        {
            _gameplayManager.onTimeUpdated += UpdateTimerVisual;
            _gameplayManager.onComboUpdated += UpdateComboVisual;
            _gameplayManager.onMissesUpdated += UpdateMissesVisual;
            _gameplayManager.onGameWin += HandleWin;
            _gameplayManager.onGameLose += HandleLose;
        }

        private void ResetGameplayManager()
        {
            _gameplayManager.onTimeUpdated -= UpdateTimerVisual;
            _gameplayManager.onComboUpdated -= UpdateComboVisual;
            _gameplayManager.onMissesUpdated -= UpdateMissesVisual;
            _gameplayManager.onGameWin -= HandleWin;
            _gameplayManager.onGameLose -= HandleLose;
        }

        private bool IsGameplayManagerValid()
        {
            if (_gameplayManager == null)
            {
                Debug.LogWarning("Please assign GameplayManager!");
                return false;
            }

            return true;
        }

        private void UpdateTimerVisual(float timeInSeconds) => _txtTimer.text = TimeSpan.FromSeconds(timeInSeconds).ToString("mm\\:ss");
        private void UpdateComboVisual(int combo) => _txtCombo.text = (combo > 1) ? $"{MainConfig.ResultScreen.COMBO} x{combo}!" : "";
        private void UpdateMissesVisual(int misses) => _txtMisses.text = $"{MainConfig.ResultScreen.MISSES}: {misses}";

        private void HandleWin(int stars, float finalTime, int maxCombo)
        {
            ShowResult(true, stars);

            _txtFinalTime.gameObject.SetActive(true);
            _txtMaxCombo.gameObject.SetActive(true);

            _txtFinalTime.text = $"{MainConfig.ResultScreen.TIME}: {TimeSpan.FromSeconds(finalTime):mm\\:ss}";
            _txtMaxCombo.text = $"{MainConfig.ResultScreen.MAX_COMBO}: {maxCombo}";
        }

        private void HandleLose() => ShowResult(false, 0);

        private void ShowResult(bool isWin, int stars)
        {
            _containerResult.SetActive(true);
            //if (_panelAnimator != null) _panelAnimator.SetTrigger("Show");

            _txtResultTitle.text = isWin ? MainConfig.ResultScreen.WIN : "GAME OVER";
            //_txtResultTitle.color = isWin ? Color.green : Color.red;

            ResetResultStars();

            bool hasNextLevel = false;
            if (SessionState.selectedLevelLibraryData != null)
            {
                int nextIndex = SessionState.selectedLevelIndex + 1;
                hasNextLevel = nextIndex < SessionState.selectedLevelLibraryData.GetLevelDataSO().Count;
            }

            _btnNext.interactable = (isWin && hasNextLevel);

            // TODO animate stars
            if (isWin)
                ShowResultStars(stars);
        }

        private void ResetResultStars() => _listStars.ForEach(star => star.SetActive(false));
        private void ShowResultStars(int totalStars)
        {
            for (int i = 0; i < totalStars; i++)
                _listStars[i].SetActive(true);
        }

        private void ResetResultButton()
        {
            _btnRetry.onClick.RemoveAllListeners();
            _btnMenu.onClick.RemoveAllListeners();
            _btnNext.onClick.RemoveAllListeners();
        }

        private void OnRetryClicked() => SceneManager.LoadScene(MainConfig.SceneName.SCENE_GAMEPLAY);
        private void OnMenuClicked() => SceneManager.LoadScene(MainConfig.SceneName.SCENE_MAIN_MENU);
        private void OnNextClicked()
        {
            if (SessionState.selectedLevelLibraryData == null) return;

            int nextIndex = SessionState.selectedLevelIndex + 1;
            var allLevels = SessionState.selectedLevelLibraryData.GetLevelDataSO();

            if (nextIndex < allLevels.Count)
            {
                SessionState.selectedLevelIndex = nextIndex;
                SessionState.selectedLevelData = allLevels[nextIndex];
                SceneManager.LoadScene(MainConfig.SceneName.SCENE_GAMEPLAY);
            }
        }

        private void AddButtonListener(Button button, UnityAction onClicked) => button.onClick.AddListener(onClicked);
        private void RemoveAllButtonListener(Button button) => button.onClick.RemoveAllListeners();
    }
}
