using DAP.Runtime.Data;
using System.Collections;
using UnityEngine;

namespace DAP.Runtime.Core
{
    public class NormalCard : BaseCard
    {
        private float _flipDuration = .2f;

        public override void Reveal()
        {
            if (isRevealed || isMatched || _isFlipping) return;

            isRevealed = true;
            StartCoroutine(FlipRoutine(_faceSprite));

            AudioDispatcher.PlaySFX(SFXType.CardFlip);
        }

        public override void Hide()
        {
            if (!isRevealed || isMatched || _isFlipping) return;

            isRevealed = false;
            StartCoroutine(FlipRoutine(_backSprite));
        }

        public override void OnMatched()
        {
            isMatched = true;
            _btnCard.interactable = false;

            _imgCard.color = new Color(1f, 1f, 1f, 0.6f);

            AudioDispatcher.PlaySFX(SFXType.Match);
        }

        public override void OnMismatched()
        {
            Hide();
            AudioDispatcher.PlaySFX(SFXType.Mismatch);
        }

        private IEnumerator FlipRoutine(Sprite targetSprite)
        {
            _isFlipping = true;
            Vector3 scale = transform.localScale;
            float maxScale = 1.15f;

            float time = 0;
            while (time < _flipDuration)
            {
                float t = time / _flipDuration;

                scale.x = Mathf.Lerp(1f, 0f, t);
                scale.y = Mathf.Lerp(1f, maxScale, t);

                transform.localScale = scale;
                time += Time.deltaTime;
                yield return null;
            }

            scale.x = 0;
            scale.y = maxScale;
            transform.localScale = scale;
            _imgCard.sprite = targetSprite;

            _isFlipping = false;

            time = 0;
            while (time < _flipDuration)
            {
                float t = time / _flipDuration;

                scale.x = Mathf.Lerp(0f, 1f, t);
                scale.y = Mathf.Lerp(maxScale, 1f, t);

                transform.localScale = scale;
                time += Time.deltaTime;
                yield return null;
            }

            transform.localScale = Vector3.one;

        }
    }
}