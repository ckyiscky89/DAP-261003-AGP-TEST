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

        private List<BaseCard> _allCards = new List<BaseCard>();
        private BaseCard _firstSelected;
        private BaseCard _secondSelected;

        private bool _isProcessing;
        private int _matchesFound;
        private int _totalPairs;

        private LevelDataSO _currentLevelData;
        private DeckDataSO _currentDeckData;

        private ISaveProvider _saveProvider;

        public Action<int> onScoreChanged;
        public Action<int> onLevelFinished;

        private int _currentScore;

        private void Awake()
        {
            _saveProvider = new JsonSaveProvider(); // switchable save provider
            _saveProvider.Load();
        }

        private void Start()
        {
            InitializeLevelData();
            InitializeCardData();

            if (_currentLevelData == null || _currentDeckData == null)
            {
                Debug.LogError("Data Null - Back to main menu");
                BackToMainMenu();
                return;
            }

            StartGame(_currentLevelData, _currentDeckData);
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
            _matchesFound = 0;
            _totalPairs = (levelLayout.y * levelLayout.x) / 2;
            _isProcessing = false;

            _levelGenerator.Generate(levelData, deckData, OnCardClicked);
        }

        private void OnCardClicked(BaseCard card)
        {
            // 1. Guard Clauses (Penting buat Tech Test!)
            //if (_isProcessing || //.IsMatched || card == _firstSelected) return;

            //card.Reveal();

            //if (_firstSelected == null)
            //{
            //    _firstSelected = card;
            //}
            //else
            //{
            //    _secondSelected = card;
            //    StartCoroutine(CheckMatchRoutine());
            //}
        }

        private IEnumerator CheckMatchRoutine()
        {
            //_isProcessing = true; // LOCK INPUT

            //// Beri waktu animasi Reveal selesai
            yield return new WaitForSeconds(0.5f);

            //if (_firstSelected.Id == _secondSelected.Id)
            //{
            //    // MATCH!
            //    _firstSelected.OnMatched();
            //    _secondSelected.OnMatched();
            //    _matchesFound++;

            //    _uiManager.AddScore(_currentLevel.BaseScorePerMatch);

            //    if (_matchesFound >= _totalPairs)
            //    {
            //        OnLevelComplete();
            //    }
            //}
            //else
            //{
            //    // MISMATCH!
            //    yield return new WaitForSeconds(0.5f); // Biar player hapalin dulu
            //    _firstSelected.OnMismatched();
            //    _secondSelected.OnMismatched();

            //    _uiManager.RegisterMiss(); // kalkulasi bintang nanti
            //}

            //// Reset selection
            //_firstSelected = null;
            //_secondSelected = null;
            //_isProcessing = false; // UNLOCK INPUT
        }

        private void OnLevelComplete()
        {
            //Debug.Log("Level Clear!");
            //_uiManager.ShowWinPanel();
            //// Panggil SaveSystem di sini
        }
    }
}