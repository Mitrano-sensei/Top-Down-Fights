using FiniteStateMachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Squares
{
    public class SquareController : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private List<Timer> _timers;

        [SerializeField] private UnityEvent<IState> _onStateChanged = new();

        private void Awake()
        {
            _stateMachine = new StateMachine();

            // Declare states
            var redState = new RedState(gameObject);
            var blueState = new BlueState(gameObject);
            var greenState = new GreenState(gameObject);

            // Declare Timers
            var greenTimer = new CountdownTimer(3f);
            var blueTimer = new CountdownTimer(2f);
            var redTimer = new CountdownTimer(4f);

            _timers = new List<Timer>(3) { greenTimer, blueTimer, redTimer };

            greenState.OnEnterAction += () => greenTimer.Start();
            redState.OnEnterAction += () => redTimer.Start();
            blueState.OnEnterAction += () => blueTimer.Start();

            // Define transitions
            To(blueState, new FuncPredicate(() => Input.GetKeyDown(KeyCode.B)));
            To(redState, new FuncPredicate(() => Input.GetKeyDown(KeyCode.R)));
            To(greenState, new FuncPredicate(() => Input.GetKeyDown(KeyCode.G)));

            At(greenState, blueState, new CountdownTimerPredicate(greenTimer));
            At(blueState, redState, new CountdownTimerPredicate(blueTimer));
            At(redState, greenState, new CountdownTimerPredicate(redTimer));

            // Set initial state
            _stateMachine.SetState(redState);
            _stateMachine.OnStateChanged += _onStateChanged.Invoke;
        }

        void Update()
        {
            _stateMachine.Update();
            _timers.ForEach(t => t.Tick(Time.deltaTime));
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        /**
         * Creates a transition from one state to another, with a specified condition
         */
        void At(IState from, IState to, IPredicate condition)
        {
            _stateMachine.AddTransition(from, to, condition);
        }

        /**
         * Creates a transition from any state to another, with a specified condition
         */
        void To(IState to, IPredicate condition)
        {
            _stateMachine.AddAnyTransition(to, condition);
        }

    }
}
