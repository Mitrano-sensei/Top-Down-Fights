using FiniteStateMachine;
using System;
using UnityEngine;
using Utilities;

namespace Squares
{
    [Serializable]
    public class  HoverTransitionDefinition : TransitionDefinition
    {
        [SerializeField] private Hoverable2D _hoverable;

        public HoverTransitionDefinition(){}

        public HoverTransitionDefinition(Hoverable2D hoverable)
        {
            _hoverable = hoverable;
        }

        public override IPredicate GetPredicate()
        {
            return new HoverPredicate(_hoverable);
        }
    }


}
