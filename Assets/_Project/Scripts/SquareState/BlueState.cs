using DG.Tweening;
using UnityEngine;

namespace Squares
{

    public class BlueState : SquareState
    {
        private Vector3 _baseScale;
        private Sequence _sequence;

        public BlueState(Transform transform) : base(transform)
        {
            _baseScale = transform.localScale;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _sequence = DOTween.Sequence();


            var renderer = _transform.GetComponent<Renderer>();
            var myColor = renderer.material.color;

            // Tween color
            renderer.material.DOColor(Color.blue, 0.5f);

            // Tween scale
            _sequence.Append(_transform.DOScale(1.1f * _baseScale, 0.5f).SetEase(Ease.InOutFlash));
            _sequence.Append(_transform.DOScale(1f * _baseScale, 1.5f).SetEase(Ease.InOutQuart));
            _sequence.SetLoops(-1);
        }

        public override void OnExit()
        {
            base.OnExit();

            _sequence.Kill();

            _transform.DOScale(1f, 0.1f).SetEase(Ease.Linear);

        }
    }
}
