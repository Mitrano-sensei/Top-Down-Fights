using FiniteStateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Square
{
    [Serializable]
    public class MultiTransitionDefinition : TransitionDefinition
    {
        [SerializeReference, SubclassSelector] private List<TransitionDefinition> _transitionDefinitions;

        public MultiTransitionDefinition()
        {
            _transitionDefinitions = new List<TransitionDefinition>();
        }

        public void Add(TransitionDefinition transition)
        {
            _transitionDefinitions.Add(transition);
        }

        public override IPredicate GetPredicate()
        {
            return new FuncPredicate(() => _transitionDefinitions.TrueForAll(transition => transition.GetPredicate().Evaluate()));
        }
    }


}
