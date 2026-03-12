using DAP.Runtime.Core;
using TMPro;
using UnityEngine;

namespace DAP.Runtime.UI
{
    public class GameUIManager : MonoBehaviour
    {
        [Header("Core Reference")]
        [SerializeField] private GameplayManager _gameplayManager;

        [Header("UI Reference")]
        [SerializeField] private TMP_Text _txtScore;
        [SerializeField] private TMP_Text _txtMisses;
        [SerializeField] private GameObject _containerWin;
        [SerializeField] private GameObject _containerLose;
        [SerializeField] private TMP_Text _txtWinStars;

        private void OnEnable()
        {
            if (_gameplayManager == null) return;

            _gameplayManager.onScoreUpdated += UpdateScore;
            _gameplayManager.onMissesUpdated += UpdateMisses;
            _gameplayManager.onGameWin += ShowWinPanel;
            _gameplayManager.onGameLose += ShowLosePanel;
        }

        private void OnDisable()
        {
            if (_gameplayManager == null) return;

            _gameplayManager.onScoreUpdated -= UpdateScore;
            _gameplayManager.onMissesUpdated -= UpdateMisses;
            _gameplayManager.onGameWin -= ShowWinPanel;
            _gameplayManager.onGameLose -= ShowLosePanel;
        }

        private void Start()
        {
            _containerWin.SetActive(false);
            _containerLose.SetActive(false);
            UpdateScore(0);
            UpdateMisses(0);
        }

        private void UpdateScore(int score) => _txtScore.text = $"{MainConfig.GeneralNaming.SCORE}: {score}";

        private void UpdateMisses(int misses) => _txtMisses.text = $"{MainConfig.GeneralNaming.MISSES}: {misses}";

        private void ShowWinPanel(int stars)
        {
            _containerWin.SetActive(true);
            _txtWinStars.text = $"{MainConfig.GeneralNaming.STARS_EARNED}: {stars}/3";
        }

        private void ShowLosePanel()
        {
            _containerLose.SetActive(true);
        }
    }
}
