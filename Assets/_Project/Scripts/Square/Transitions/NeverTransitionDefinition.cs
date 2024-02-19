using FiniteStateMachine;
using System;

namespace Squares
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
