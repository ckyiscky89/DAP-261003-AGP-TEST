using UnityEngine;

namespace DAP.Runtime.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "DAP/Data/New Level Data")]
    public class LevelDataSO : ScriptableObject
    {
        [SerializeField] private string _levelId;
        [SerializeField] private string _levelName;

        [Header("Grid Settings")]
        [Tooltip("X = Number of Columns, Y = Number of Rows")]
        [SerializeField] private Vector2Int _gridLayout = new(2,2);

        [Header("Level Rules")]
        [SerializeField] private float _timeLimit = 30;
        [SerializeField] private float _memorizeTime = 1;
        [SerializeField] private float _maxMistake = 5;
        [SerializeField] private int _baseScorePerMatch = 100;

        public string GetLevelName() => _levelName;
        public Vector2Int GetGridLayout() => _gridLayout;
        public float GetTimeLimit() => _timeLimit;
        public float GetMemorizeTime() => _memorizeTime;
        public float GetMaxMistake() => _maxMistake;
        public int GetScorePerMatch() => _baseScorePerMatch;
    }
}