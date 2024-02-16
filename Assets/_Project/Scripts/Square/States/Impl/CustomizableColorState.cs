using DG.Tweening;
using System;
using UnityEngine;

namespace Squares
{
    [Serializable]
    public class CustomizableColorState : SquareState
    {
        [Header("Customization ! :D")]
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private float _baseScaleFactor = .6f;      // TODO : We could make a list of scales/eases to make it more customizable
        [SerializeField] private float _finalScaleFactor = 1.4f;
        [SerializeField, Range(0.05f, 3f)] private float _firstScaleDuration = 1f;
        [SerializeField, Range(0.05f, 3f)] private float _secondScaleDuration = 1f;
        [SerializeField] private Ease _firstScaleEase = Ease.InSine;
        [SerializeField] private Ease _secondScaleEase = Ease.InOutBounce;

        public Color Color => _color;

        public CustomizableColorState(Transform transform) : base(transform) { }
        public CustomizableColorState() { }

        public override void OnEnter()
        {
            base.OnEnter();
            _transform.DOScale(_baseScaleFactor * _baseScale, _firstScaleDuration).SetEase(_firstScaleEase);
            _sequence = DOTween.Sequence();

            var renderer = _transform.GetComponent<SpriteRenderer>();

            // Tween color
            renderer.DOColor(_color, 0.5f).SetEase(Ease.Linear);

            // Tween scale
            _sequence.Append(_transform.DOScale(_finalScaleFactor * _baseScale, _secondScaleDuration).SetEase(_secondScaleEase));
            _sequence.SetLoops(-1, LoopType.Yoyo);
        }

    }
}
