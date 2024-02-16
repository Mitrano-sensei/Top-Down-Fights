using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FiniteStateMachine
{
    public class StateMachine
    {
        StateNode _current;
        Dictionary<string, StateNode> _nodes = new();
        HashSet<ITransition> _anyTransitions = new();

        public UnityAction<IState> OnStateChanged = delegate { };
        public UnityAction<float> OnUpdateAction = delegate { };
        public UnityAction<float> OnFixedUpdateAction = delegate { };

        [SerializeField] private bool _isDebug = false;
        public bool IsDebug { get => _isDebug; set => _isDebug = value; }

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
                ChangeState(transition.To);

            _current.State?.Update();
            OnUpdateAction?.Invoke(Time.deltaTime);
        }

        public void FixedUpdate()
        {
            _current.State?.FixedUpdate();
            OnFixedUpdateAction?.Invoke(Time.fixedDeltaTime);
        }

        public void SetState(IState state)
        {
            _current = _nodes[state.Name];
            _current.State?.OnEnter();

            OnStateChanged?.Invoke(_current.State);
        }

        void ChangeState(IState state)
        {
            if (state == _current.State) return;

            var previousState = _current.State;
            var nextState = _nodes[state.Name].State;

            previousState?.OnExit();
            nextState?.OnEnter();
            _current = _nodes[state.Name];
            
            OnStateChanged?.Invoke(_current.State);
        }

        ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition.Evaluate())
                    return transition;

            foreach (var transition in _current.Transitions)
                if (transition.Condition.Evaluate())
                    return transition;

            return null;
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        StateNode GetOrAddNode(IState state)
        {
            // Dump info
            if (_isDebug) Debug.Log($"State: {state.Name}, Type : {state.GetType()}");

            var node = _nodes.GetValueOrDefault(state.Name);

            if (node == null)
            {
                node = new StateNode(state);
                _nodes[state.Name] = node;
            }

            return node;
        }

        class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }

}
