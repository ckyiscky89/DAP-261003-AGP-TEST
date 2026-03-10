using UnityEngine;

namespace DAP.Runtime.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "DAP/New Level Data")]
    public class LevelDataSO : ScriptableObject
    {
        public string levelName;
    }
}