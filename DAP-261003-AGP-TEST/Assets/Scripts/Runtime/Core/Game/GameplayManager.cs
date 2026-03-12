using DAP.Runtime.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DAP.Runtime.Core
{
    public class GameplayManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private LevelGenerator _levelGenerator;

        public Action<int> onScoreUpdated;
        public Action<int> onMissesUpdated;
        public Action<int> onGameWin;
        public Action onGameLose;

        private BaseCard _firstSelected;
        private BaseCard _secondSelected;

        private List<BaseCard> _activeCards = new List<BaseCard>();

        private bool _isProcessing;
        private int _matchesFound;
        private int _totalPairs;
        private int _totalMisses;
        private int _currentScore;

        private LevelDataSO _currentLevelData;
        private DeckDataSO _currentDeckData;

        private ISaveProvider _saveProvider;

        private void Start()
        {
            InitializeSaveProvider();
            InitializeLevelData();
            InitializeCardData();

            if (_currentLevelData == null || _currentDeckData == null)
            {
                Debug.LogWarning("Data Null - Restart ... - Back to main menu");
                BackToMainMenu();
                return;
            }

            StartGame(_currentLevelData, _currentDeckData);
        }

        private void InitializeSaveProvider()
        {
            _saveProvider = new JsonSaveProvider(); // switchable save provider
            _saveProvider.Load();
        }

        private void InitializeLevelData()
        {
            _currentLevelData = SessionState.selectedLevelData;
        }

        private void InitializeCardData()
        {
            _currentDeckData = SessionState.selectedDeckData;
        }

        private void BackToMainMenu() => SceneManager.LoadScene(MainConfig.SceneName.SCENE_MAIN_MENU);

        public void StartGame(LevelDataSO levelData, DeckDataSO deckData)
        {
            var levelLayout = levelData.GetGridLayout();
            _totalPairs = (levelLayout.y * levelLayout.x) / 2;
            _matchesFound = 0;
            _totalMisses = 0;
            _currentScore = 0;
            _isProcessing = true;

            _activeCards = _levelGenerator.Generate(levelData, deckData, OnCardClicked);

            StartCoroutine(MemorizePhaseRoutine());
        }

        private IEnumerator MemorizePhaseRoutine()
        {
            foreach (var card in _activeCards) 
                card.Reveal();

            yield return new WaitForSeconds(_currentLevelData.GetMemorizeTime());

            foreach (var card in _activeCards) 
                card.Hide();

            yield return new WaitForSeconds(0.3f);

            _isProcessing = false;
        }

        private void OnCardClicked(BaseCard card)
        {
            if (_isProcessing || card.IsRevealed || card.IsMatched) return;

            card.Reveal();

            if (_firstSelected == null)
            {
                _firstSelected = card;
            }
            else
            {
                _secondSelected = card;
                StartCoroutine(CheckMatchRoutine());
            }
        }

        private IEnumerator CheckMatchRoutine()
        {
            _isProcessing = true;

            yield return new WaitForSeconds(0.5f);

            bool isMatched = _firstSelected.cardId == _secondSelected.cardId;
            if (isMatched)
            {
                _firstSelected.OnMatched();
                _secondSelected.OnMatched();
                _matchesFound++;

                _currentScore += _currentLevelData.GetScorePerMatch();
                onScoreUpdated?.Invoke(_currentScore);

                if (_matchesFound >= _totalPairs)
                {
                    HandleWin();
                }
            }
            else
            {
                _totalMisses++;
                onMissesUpdated?.Invoke(_totalMisses);

                _firstSelected.OnMismatched();
                _secondSelected.OnMismatched();

                if (_totalMisses >= _currentLevelData.GetMaxMistake())
                {
                    HandleLose();
                }
            }

            _firstSelected = null;
            _secondSelected = null;
            _isProcessing = false;
        }

        private void HandleWin()
        {
            _isProcessing = true;

            float missRatio = (float)_totalMisses / _currentLevelData.GetMaxMistake();
            int stars = (missRatio <= 0.2f) ? 3 : (missRatio <= 0.6f) ? 2 : 1;

            _saveProvider.SaveStars(SessionState.selectedLevelIndex, stars);

            onGameWin?.Invoke(stars);
        }

        private void HandleLose()
        {
            _isProcessing = true;
            onGameLose?.Invoke();
        }
    }
}