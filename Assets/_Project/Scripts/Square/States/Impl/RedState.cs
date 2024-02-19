using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Squares
{
    [Serializable]
    public class RedState : SquareState
    {
        public RedState(GameObject gameObject) : base(gameObject){}
        public RedState() { }


        public override void OnEnter()
        {
            base.OnEnter();
            var transform = _gameObject.transform;
            transform.DOScale(1f * _baseScale, 0.1f).SetEase(Ease.Linear);
            _sequence = DOTween.Sequence();

            var renderer = transform.GetComponent<SpriteRenderer>();

            // Tween color
            renderer.DOColor(Color.red, 0.5f);

            // Tween scale
            _sequence.Append(transform.DOScale(1.1f * _baseScale, 0.5f).SetEase(Ease.OutSine));
            _sequence.Append(transform.DOScale(1f * _baseScale, 0.5f).SetEase(Ease.InOutBounce));
            _sequence.SetLoops(-1, LoopType.Yoyo);
        }

    }
}
