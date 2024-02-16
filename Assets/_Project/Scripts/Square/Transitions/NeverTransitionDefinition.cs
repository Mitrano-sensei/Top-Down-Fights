using FiniteStateMachine;
using System;

namespace Square
{
    [Serializable]
    public class NeverTransitionDefinition : TransitionDefinition
    {
        public override IPredicate GetPredicate()
        {
            return new FuncPredicate(() => false); ;
        }
    }


}
