using System;
using UnityEngine;
using UnityEngine.UI;

namespace DAP.Runtime.Core
{
    public abstract class BaseCard : MonoBehaviour
    {
        public int cardId { get; protected set; }
        public bool isMatched { get; protected set; }
        public bool isRevealed { get; protected set; }

        [Header("UI References")]
        [SerializeField] protected Image _imgCard;
        [SerializeField] protected Button _btnCard;

        protected bool _isFlipping;
        protected Sprite _faceSprite;
        protected Sprite _backSprite;
        protected Action<BaseCard> _onClicked;

        public virtual void Initialize(int id, Sprite face, Sprite back, Action<BaseCard> callback)
        {
            cardId = id;

            _faceSprite = face; 
            _backSprite = back; 
            _onClicked = callback;

            _imgCard.sprite = back;

            _btnCard.onClick.RemoveAllListeners();
            _btnCard.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            if (isMatched || isRevealed) 
                return;

            _onClicked?.Invoke(this);
        }

        public abstract void Reveal();
        public abstract void Hide();

        public abstract void OnMismatched();
        public abstract void OnMatched();
    }
}
