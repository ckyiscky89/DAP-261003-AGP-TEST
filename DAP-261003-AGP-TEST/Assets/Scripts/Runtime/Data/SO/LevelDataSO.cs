using UnityEngine;

namespace DAP.Runtime.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "DAP/New Level Data")]
    public class LevelDataSO : ScriptableObject
    {
        public string levelName;

        [Header("Grid Settings")]
        [Tooltip("X = Number of Columns, Y = Number of Rows")]
        public Vector2 layout = new(2,2);

        [Header("Level Rules")]
        public float timeLimit = 30;
        public float memorizeTime = 1;
        public float maxMistake = 5;
    }
}