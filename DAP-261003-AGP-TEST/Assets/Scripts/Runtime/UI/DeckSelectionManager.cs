using UnityEngine;
using DAP.Runtime.Data;
using DAP.Runtime.Core;
using System.Collections.Generic; // Boleh akses Core untuk SessionState

namespace DAP.Runtime.UI
{
    public class DeckSelectionManager : MonoBehaviour
    {
        // TODO Deck Library
        [Header("Data")]
        [SerializeField] private List<DeckDataSO> _listDecks;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_listDecks.Count > 0 && SessionState.selectedDeckData == null)
            {
                SessionState.selectedDeckData = _listDecks[0];
            }
        }

        public void SetDeck(int deckIndex)
        {
            if (deckIndex < 0 || deckIndex >= _listDecks.Count)
            {
                Debug.LogError("[DeckSelectionManager] Invalid theme index!");
                return;
            }

            SessionState.selectedDeckData = _listDecks[deckIndex];
        }
    }
}