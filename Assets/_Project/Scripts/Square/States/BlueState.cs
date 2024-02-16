using DG.Tweening;
using System;
using UnityEngine;

namespace Squares
{
    [Serializable]
    public class BlueState : SquareState
    {
        public BlueState(Transform transform) : base(transform){}
        public BlueState() { }

        public override void OnEnter()
        {
            base.OnEnter();
            _transform.DOScale(1f * _baseScale, 0.1f).SetEase(Ease.Linear);
            _sequence = DOTween.Sequence();

            var renderer = _transform.GetComponent<SpriteRenderer>();

            // Tween color
            renderer.DOColor(Color.blue, 0.5f).SetEase(Ease.Linear);

            // Tween scale
            _sequence.Append(_transform.DOScale(.9f * _baseScale, 0.5f).SetEase(Ease.InSine));
            _sequence.Append(_transform.DOScale(1f * _baseScale, 1.5f).SetEase(Ease.InOutBounce));
            _sequence.SetLoops(-1, LoopType.Yoyo);
        }

        public override void OnExit()
        {
            base.OnExit();

            _sequence.Kill();
        }
    }
}
