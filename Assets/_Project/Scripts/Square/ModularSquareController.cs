using FiniteStateMachine;
using Squares;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Square
{
    public class ModularSquareController : MonoBehaviour
    {
        [SerializeField] private List<SquareStateDefinition> _stateDefinitions;
        private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new StateMachine();

            for (var i = 0; i < _stateDefinitions.Count; i++)
            {
                var current = _stateDefinitions[i];
                var nextIndex = (i + 1) % _stateDefinitions.Count;
                var next = _stateDefinitions[nextIndex];

                var transition = _stateDefinitions[i].EndCondition;

                Init(current);

                At(current, next, transition.GetPredicate());

                var onExitAction = transition.GetOnExitAction();
                var onEnterAction = transition.GetOnEnterAction();
                var onUpdateAction = transition.GetOnUpdateAction();
                var onFixedUpdateAction = transition.GetOnFixedUpdateAction();

                if (onExitAction != null)           current.SquareState.OnExitAction  += transition.GetOnExitAction();
                if (onEnterAction != null)          current.SquareState.OnEnterAction += transition.GetOnEnterAction();

                if (onUpdateAction != null)         _stateMachine.OnUpdateAction        += transition.GetOnUpdateAction();
                if (onFixedUpdateAction != null)    _stateMachine.OnFixedUpdateAction   += transition.GetOnFixedUpdateAction();

                
            }

            _stateMachine.SetState(_stateDefinitions[0].SquareState);
            
        }

        void Update()
        {
            _stateMachine.Update();
        }

        void FixedUpdate()
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

        void At(SquareStateDefinition from, SquareStateDefinition to, IPredicate condition)
        {
            At(from.SquareState, to.SquareState, condition);
        }

        /**
         * Creates a transition from any state to another, with a specified condition
         */
        void To(IState to, IPredicate condition)
        {
            _stateMachine.AddAnyTransition(to, condition);
        }

        private void Init(SquareStateDefinition current)
        {
            var state = current.SquareState;
            state.SetTransform(transform);
            state.GenerateName();
        }

    }

    [Serializable]
    public class SquareStateDefinition
    {
        [SerializeReference, SubclassSelector] public SquareState SquareState;
        [SerializeReference, SubclassSelector] public TransitionDefinition EndCondition;

    }

    [Serializable]
    public abstract class TransitionDefinition
    {
        public abstract IPredicate GetPredicate();
        public abstract Action GetOnExitAction();
        public abstract Action GetOnEnterAction();
        public abstract UnityAction<float> GetOnUpdateAction();
        public abstract UnityAction<float> GetOnFixedUpdateAction();
    }

    [Serializable]
    public class CountDownTransitionDefinition : TransitionDefinition
    {
        [SerializeField] private float _duration;

        private CountdownTimer Timer { get; set; }

        public override IPredicate GetPredicate()
        {
            Timer = new CountdownTimer(_duration);

            return new CountdownTimerPredicate(Timer);
        }

        public override Action GetOnExitAction()
        {
            return null;
        }

        public override Action GetOnEnterAction()
        {
            return () => Timer.Start();
        }

        public override UnityAction<float> GetOnUpdateAction()
        {
            return Timer.Tick;
        }

        public override UnityAction<float> GetOnFixedUpdateAction()
        {
            return null;
        }
    }


}
