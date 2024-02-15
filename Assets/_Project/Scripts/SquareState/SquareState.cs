using FiniteStateMachine;
using System;
using UnityEngine;

namespace Squares
{
    public abstract class SquareState : BaseState
    {
        public Action OnEnterAction = delegate { };
        public Action OnExitAction = delegate { };

        protected Transform _transform;

        public SquareState(Transform transform)
        {
            _transform = transform;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Entering " + GetType().Name);
            OnEnterAction();
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exiting " + GetType().Name);
            OnExitAction();
        }

    }
}
