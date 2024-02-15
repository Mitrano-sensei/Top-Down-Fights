using FiniteStateMachine;
using UnityEngine;

namespace Squares
{
    public abstract class SquareState : BaseState
    {
        protected Transform _transform;

        public SquareState(Transform transform)
        {
            _transform = transform;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Entering " + GetType().Name);
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exiting " + GetType().Name);
        }

    }
}
