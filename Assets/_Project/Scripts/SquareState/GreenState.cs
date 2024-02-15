using DG.Tweening;
using UnityEngine;

namespace Squares
{
    public class GreenState : SquareState
    {
        private Vector3 _baseScale;
        private Sequence _sequence;

        public GreenState(Transform transform) : base(transform)
        {
            _baseScale = transform.localScale;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _transform.DOScale(1.3f * _baseScale, 0.1f).SetEase(Ease.Linear);
            _sequence = DOTween.Sequence();

            var renderer = _transform.GetComponent<SpriteRenderer>();

            // Tween color
            renderer.DOColor(Color.green, 0.5f).SetEase(Ease.Linear);

            // Tween scale
            _sequence.Append(_transform.DOScale(1.3f * _baseScale, 0.5f).SetEase(Ease.InOutSine));
            _sequence.Append(_transform.DOScale(1.6f * _baseScale, 0.5f).SetEase(Ease.InOutBounce));
            _sequence.SetLoops(-1, LoopType.Yoyo);
        }

        public override void OnExit()
        {
            base.OnExit();

            _sequence.Kill();
        }
    }
}
