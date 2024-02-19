using DG.Tweening;
using System;
using UnityEngine;

namespace Squares
{
    [Serializable]
    public class BlueState : SquareState
    {
        public BlueState(GameObject gameObject) : base(gameObject){}
        public BlueState() { }

        public override void OnEnter()
        {
            base.OnEnter();
            var transform = _gameObject.transform;
            transform.DOScale(1f * _baseScale, 0.1f).SetEase(Ease.Linear);
            _sequence = DOTween.Sequence();

            var renderer = transform.GetComponent<SpriteRenderer>();

            // Tween color
            renderer.DOColor(Color.blue, 0.5f).SetEase(Ease.Linear);

            // Tween scale
            _sequence.Append(transform.DOScale(.9f * _baseScale, 0.5f).SetEase(Ease.InSine));
            _sequence.Append(transform.DOScale(1f * _baseScale, 1.5f).SetEase(Ease.InOutBounce));
            _sequence.SetLoops(-1, LoopType.Yoyo);
        }

    }
}
