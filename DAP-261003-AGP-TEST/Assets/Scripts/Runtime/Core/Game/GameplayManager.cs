using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAP.Runtime.Core
{
    using DAP.Runtime.Data;
    using System.Collections;
    using System.Collections.Generic;
    using TreeEditor;
    using UnityEngine;

    public class GameplayManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private LevelGenerator _levelGenerator;
        //[SerializeField] private UIManager _uiManager;

        private List<BaseCard> _allCards = new List<BaseCard>();
        private BaseCard _firstSelected;
        private BaseCard _secondSelected;

        private bool _isProcessing;
        private int _matchesFound;
        private int _totalPairs;

        private LevelDataSO _currentLevel;

        public void StartGame(LevelDataSO levelData, CardDataSO cardData)
        {
            //_currentLevel = levelData;
            //_matchesFound = 0;
            //_totalPairs = (levelData.Rows * levelData.Columns) / 2;
            //_isProcessing = false;

            //// LevelGenerator sekarang kirim callback OnCardClicked ke sini
            //_levelGenerator.Generate(levelData, cardData, OnCardClicked);
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