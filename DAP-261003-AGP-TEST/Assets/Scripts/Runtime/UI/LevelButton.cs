using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DAP.Runtime.Data;

namespace DAP.Runtime.UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _levelNameText;
        [SerializeField] private GameObject _lockOverlay;
        [SerializeField] private GameObject[] _stars;
        
        public void Setup(LevelDataSO levelData, int levelIndex, bool isLocked, int starsEarned, System.Action<LevelDataSO, int> onClick)
        {
            _levelNameText.text = levelData.GetLevelName();
            _lockOverlay.SetActive(isLocked);
            _button.interactable = !isLocked;

            for (int i = 0; i < _stars.Length; i++)
                _stars[i].SetActive(i < starsEarned);

            _button.onClick.AddListener(() => onClick?.Invoke(levelData, levelIndex));
        }
    }
}
