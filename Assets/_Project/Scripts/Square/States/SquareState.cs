using DG.Tweening;
using EasyButtons;
using FiniteStateMachine;
using log4net.Util;
using System;
using UnityEditor;
using UnityEngine;

namespace Squares
{
    [Serializable]
    public abstract class SquareState : BaseState
    {
        [Header("Base Square State Info")]

        [SerializeField] private string _name = "";

        public Action OnEnterAction = delegate { };
        public Action OnExitAction = delegate { };
        public override string Name { get => _name; protected set => _name = value; }

        protected GameObject _gameObject;

        protected Vector3 _baseScale;
        protected Sequence _sequence;

        public static int GlobalId = 0;

        [SerializeField] private bool _isDebug = false;
        public bool IsDebug => _isDebug;

        public SquareState(GameObject gameObject)
        {
            GenerateName();
            SetGameObject(gameObject);
        }

        public SquareState()
        {
            GenerateName();
        }

        public void GenerateName()
        {
            if (_name == "" || _name == null)
                _name = GetType().Name + GlobalId++;
        }

        public virtual void SetGameObject(GameObject gameObject)
        {
            _gameObject = gameObject;
            _baseScale = _gameObject.transform.localScale;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (_isDebug)
            {
                Debug.Log("Entering " + GetType().Name + " named " + _name);
            }
            OnEnterAction?.Invoke();
        }

        public override void OnExit()
        {
            base.OnExit();
            if (_isDebug) Debug.Log("Exiting " + GetType().Name);
            OnExitAction?.Invoke();

            _sequence.Kill();
        }

    }
}
