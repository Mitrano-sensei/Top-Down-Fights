using EasyButtons;
using FiniteStateMachine;
using KBCore.Refs;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Square
{
    public class ModularSquareController : MonoBehaviour
    {
        [Header("Loop Behavior")]
        [SerializeField] private List<SquareStateUntilDefinition> _stateDefinitions;

        [Header("Any Transition Behavior")]
        [SerializeField] private List<SquareStateAnyDefinition> _anyTransitionDefinition;
        [SerializeField] private bool _isDebug = false;

        private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new StateMachine();

            _stateDefinitions.ForEach(Init);

            for (var i = 0; i < _stateDefinitions.Count; i++)
            {
                var current = _stateDefinitions[i];
                var nextIndex = (i + 1) % _stateDefinitions.Count;
                var next = _stateDefinitions[nextIndex];

                var transition = _stateDefinitions[i].Until;

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

            foreach (var anyTransitionDef in _anyTransitionDefinition)
            {
                var to = _stateDefinitions.Find(s => s.SquareState.Name == anyTransitionDef.To);
                if (to == null)
                {
                    Debug.LogError($"Could not find state with name {anyTransitionDef.To}");
                    continue;
                }
                To(to.SquareState, anyTransitionDef.When.GetPredicate());
            }

            _stateMachine.SetState(_stateDefinitions[0].SquareState);
            _stateMachine.IsDebug = _isDebug;
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
            if (_isDebug) Debug.Log($"Adding transition of type {condition.GetType().Name} from {from.Name} to {to.Name}");
            _stateMachine.AddTransition(from, to, condition);
        }

        void At(SquareStateUntilDefinition from, SquareStateUntilDefinition to, IPredicate condition)
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

        private void Init(SquareStateUntilDefinition current)
        {
            var state = current.SquareState;
            state.SetTransform(transform);
            state.GenerateName();
        }

        [Button]
        public void ToggleDebug()
        {
            _isDebug = !_isDebug;

            if (Application.IsPlaying(gameObject))
            {
                _stateMachine.IsDebug = _isDebug;
            }
            
        }

    }

}
