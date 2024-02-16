using System;
using UnityEngine;

namespace FiniteStateMachine
{
    /**
     * Predicate held by a function.
     */
    public class FuncPredicate : IPredicate
    {
        private System.Func<bool> _func;

        public FuncPredicate(System.Func<bool> func)
        {
            _func = func;
        }

        public FuncPredicate()
        {

        }

        public bool Evaluate() => _func();
    }
}
