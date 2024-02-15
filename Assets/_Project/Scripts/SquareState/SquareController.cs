using FiniteStateMachine;
using UnityEngine;

namespace Squares
{
    public class SquareController : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private SquareState _currentState;

        private void Awake()
        {
            _stateMachine = new StateMachine();

            // Declare states
            var redState = new RedState(transform);
            var blueState = new BlueState(transform);

            // Define transitions
            To(blueState, new FuncPredicate(() => Input.GetKeyDown(KeyCode.B)));
            To(redState, new FuncPredicate(() => Input.GetKeyDown(KeyCode.R)));

            // Set initial state
            _stateMachine.SetState(redState);
        }

        void At(IState from, IState to, IPredicate condition)
        {
            _stateMachine.AddTransition(from, to, condition);
        }

        void To(IState to, IPredicate condition)
        {
            _stateMachine.AddAnyTransition(to, condition);
        }

        void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

    }
}
