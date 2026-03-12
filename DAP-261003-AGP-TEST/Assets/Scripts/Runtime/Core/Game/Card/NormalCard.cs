using DAP.Runtime.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAP.Runtime.Core
{
    public class NormalCard : BaseCard
    {
        private const float FLIP_DURATION = 0.2f;

        public override void Reveal()
        {
            if (IsRevealed || IsMatched || _isFlipping) return;

            IsRevealed = true;
            StartCoroutine(FlipRoutine(_faceSprite));
        }

        public override void Hide()
        {
            if (!IsRevealed || IsMatched || _isFlipping) return;

            IsRevealed = false;
            StartCoroutine(FlipRoutine(_backSprite));
        }

        public override void OnMatched()
        {
            IsMatched = true;
            _btnCard.interactable = false;

            _imgCard.color = new Color(1f, 1f, 1f, 0.6f);
        }

        public override void OnMismatched()
        {
            Hide();
        }

        private IEnumerator FlipRoutine(Sprite targetSprite)
        {
            _isFlipping = true;
            Vector3 scale = transform.localScale;

            float time = 0;
            while (time < FLIP_DURATION)
            {
                scale.x = Mathf.Lerp(1f, 0f, time / FLIP_DURATION);
                transform.localScale = scale;
                time += Time.deltaTime;
                yield return null;
            }

            scale.x = 0;
            transform.localScale = scale;
            _imgCard.sprite = targetSprite;

            time = 0;
            while (time < FLIP_DURATION)
            {
                scale.x = Mathf.Lerp(0f, 1f, time / FLIP_DURATION);
                transform.localScale = scale;
                time += Time.deltaTime;
                yield return null;
            }

            scale.x = 1;
            transform.localScale = scale;
            _isFlipping = false;
        }
    }
}