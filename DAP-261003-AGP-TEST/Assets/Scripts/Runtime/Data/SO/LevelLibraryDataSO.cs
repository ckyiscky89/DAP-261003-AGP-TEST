using System.Collections.Generic;
using UnityEngine;

namespace DAP.Runtime.Data
{
    [CreateAssetMenu(fileName = "LevelLibraryData", menuName = "DAP/Data/New Level Library")]
    public class LevelLibraryDataSO : ScriptableObject
    {
        [SerializeField] private List<LevelDataSO> _levels;

        public List<LevelDataSO> GetLevelDataSO() => _levels;
    }
}
