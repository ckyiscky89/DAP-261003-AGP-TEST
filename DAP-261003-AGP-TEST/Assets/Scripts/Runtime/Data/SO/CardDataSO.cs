using System.Collections.Generic;
using UnityEngine;

namespace DAP.Runtime.Data
{
    [CreateAssetMenu(fileName = "CardData", menuName = "DAP/Data/New Card Data")]
    public class CardDataSO : ScriptableObject
    {
        [SerializeField] private string _cardId;
        [SerializeField] private Sprite _cardBack;
        [SerializeField] private List<Sprite> _listCardFaces;

        public Sprite GetCardBack() => _cardBack;
        public List<Sprite> GetListCardFaces() => _listCardFaces;
    }
}
