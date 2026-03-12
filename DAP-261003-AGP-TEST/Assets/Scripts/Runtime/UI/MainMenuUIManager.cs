using UnityEngine;
using UnityEngine.UI;

namespace DAP.Runtime.UI
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [Header("Main Menu Reference")]
        [SerializeField] private GameObject _containerMainMenu;
        [SerializeField] private Button _btnStartGame;

        [Header("Level Selection Reference")]
        [SerializeField] private GameObject _containerLevelSelection;
        [SerializeField] private LevelSelectionManager _levelSelectionManager;

        private void Start()
        {
            InitializeMainMenu();
            InitializeButtonStartGame();
        }

        private void InitializeMainMenu()
        {
            if (_containerMainMenu == null || _containerLevelSelection == null || _levelSelectionManager == null)
            {
                Debug.LogError("[MainMenuUIManager] Please assign required containers in the Inspector!");
                return;
            }

            _containerMainMenu.SetActive(true);
            _containerLevelSelection.SetActive(false);
        }

        private void InitializeButtonStartGame()
        {
            if (_btnStartGame == null)
            {
                Debug.LogError("[MainMenuUIManager] Start Game button is missing!");
                return;
            }

            _btnStartGame.onClick.AddListener(OnStartGameClicked);
        }

        private void OnStartGameClicked()
        {
            _containerMainMenu.SetActive(false);
            _containerLevelSelection.SetActive(true);

            _levelSelectionManager.Initialize();
        }

        private void OnDestroy()
        {
            _btnStartGame?.onClick.RemoveListener(OnStartGameClicked);
        }
    }
}
