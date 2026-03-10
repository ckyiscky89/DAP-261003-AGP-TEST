using System.Collections.Generic;
using UnityEngine;

namespace DAP.Runtime.Data
{
    [CreateAssetMenu(fileName = "CardData", menuName = "DAP/DATA/New Card Data")]
    public class CardDataSO : ScriptableObject
    {
        [SerializeField] private Sprite _cardBack;
        [SerializeField] private List<Sprite> _listCardFaces;

        public Sprite GetCardBack() => _cardBack;
        public List<Sprite> GetListCardFaces() => _listCardFaces;
    }
}
