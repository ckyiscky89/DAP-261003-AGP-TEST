using DAP.Runtime.Data;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DAP.Runtime.Core
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private GridLayoutGroup _gridLayout;

        public void Generate(LevelDataSO levelData, CardDataSO cardData, System.Action<BaseCard> onCardClick)
        {
            ClearGrid();

            Vector2Int gridLayout = levelData.GetGridLayout();
            int totalSlots = gridLayout.y * gridLayout.x;
            bool hasHole = totalSlots % 2 != 0;

            List<int> cardIds = GetCardIds(totalSlots);
            Shuffle(cardIds);

            _gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayout.constraintCount = gridLayout.x;

            int holeIndex = hasHole ? Random.Range(0, totalSlots) : -1;
            int idPointer = 0;

            Transform gridParent = _gridLayout.transform;

            for (int i = 0; i < totalSlots; i++)
            {
                if (i == holeIndex)
                {
                    GameObject hole = new GameObject("_spacer_");
                    hole.transform.SetParent(gridParent, false);
                    hole.AddComponent<LayoutElement>();
                    continue;
                }

                GameObject go = Instantiate(_cardPrefab, gridParent);
                BaseCard card = go.GetComponent<BaseCard>();

                int id = cardIds[idPointer++];
                card.Initialize(id, cardData.GetListCardFaces()[id], cardData.GetCardBack(), onCardClick);
            }
        }

        private List<int> GetCardIds(int totalSlots)
        {
            int pairCount = totalSlots / 2;
            List<int> result = new List<int>();

            for (int i = 0; i < pairCount; i++)
            {
                result.Add(i);
                result.Add(i);
            }

            return result;
        }

        private void Shuffle<T>(List<T> list) { /* Logic Acak List */ }
        private void ClearGrid() { /* Hapus sisa kartu lama */ }
    }
}
