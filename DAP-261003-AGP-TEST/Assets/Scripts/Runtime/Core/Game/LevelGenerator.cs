using DAP.Runtime.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DAP.Runtime.Core
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private GridLayoutGroup _gridLayout;

        public List<BaseCard> Generate(LevelDataSO levelData, DeckDataSO deckData, System.Action<BaseCard> onCardClick)
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
            List<BaseCard> generatedCards = new List<BaseCard>();

            var cardFaces = deckData.GetListCardFaces();
            if (cardFaces == null || cardFaces.Count == 0)
            {
                Debug.LogError("[LevelGenerator] DeckDataSO has no card faces!");
                return generatedCards;
            }

            for (int i = 0; i < totalSlots; i++)
            {
                if (i == holeIndex)
                {
                    GameObject hole = new GameObject(MainConfig.GeneralNaming.SPACER);
                    hole.transform.SetParent(gridParent, false);
                    hole.AddComponent<LayoutElement>();
                    continue;
                }

                GameObject go = Instantiate(_cardPrefab, gridParent);
                BaseCard card = go.GetComponent<BaseCard>();

                int id = cardIds[idPointer++];

                // SAFETY CHECK (Modulo): Avoid IndexOutOfRangeException 
                // kalo butuh 10 pair kartu tapi deck cuma 5
                Sprite faceSprite = cardFaces[id % cardFaces.Count];

                card.Initialize(id, faceSprite, deckData.GetCardBack(), onCardClick);

                generatedCards.Add(card);
            }

            return generatedCards;
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

        // Fisher Yates Shuffle
        private void Shuffle<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = Random.Range(i, list.Count);
                T temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        private void ClearGrid()
        {
            foreach (Transform child in _gridLayout.transform)
                Destroy(child.gameObject);
        }
    }
}