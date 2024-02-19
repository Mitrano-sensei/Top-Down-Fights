using DG.Tweening;
using System;
using UnityEngine;

namespace Squares
{
    [Serializable]
    public class GreenState : SquareState
    {

        public GreenState(GameObject gameObject) : base(gameObject){}

        public GreenState(){}

        public override void OnEnter()
        {
            base.OnEnter();
            var transform = _gameObject.transform;
            transform.DOScale(1.3f * _baseScale, 0.1f).SetEase(Ease.Linear);
            _sequence = DOTween.Sequence();

            var renderer = transform.GetComponent<SpriteRenderer>();

            // Tween color
            renderer.DOColor(Color.green, 0.5f).SetEase(Ease.Linear);

            // Tween scale
            _sequence.Append(transform.DOScale(1.3f * _baseScale, 0.5f).SetEase(Ease.InOutSine));
            _sequence.Append(transform.DOScale(1.6f * _baseScale, 0.5f).SetEase(Ease.InOutBounce));
            _sequence.SetLoops(-1, LoopType.Yoyo);
        }
    }
}
