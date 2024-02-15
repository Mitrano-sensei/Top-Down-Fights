using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Squares
{

    public class RedState : SquareState
    {
        private Vector3 _baseScale;
        private Sequence _sequence;

        public RedState(Transform transform) : base(transform)
        {
            _baseScale = transform.localScale;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _transform.DOScale(1f * _baseScale, 0.1f).SetEase(Ease.Linear);
            _sequence = DOTween.Sequence();

            var renderer = _transform.GetComponent<SpriteRenderer>();

            // Tween color
            renderer.DOColor(Color.red, 0.5f);

            // Tween scale
            _sequence.Append(_transform.DOScale(1.1f * _baseScale, 0.5f).SetEase(Ease.OutSine));
            _sequence.Append(_transform.DOScale(1f * _baseScale, 0.5f).SetEase(Ease.InOutBounce));
            _sequence.SetLoops(-1, LoopType.Yoyo);
        }

        public override void OnExit()
        {
            base.OnExit();

            _sequence.Kill();
        }


    }
}
