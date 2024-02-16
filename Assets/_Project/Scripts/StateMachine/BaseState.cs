using System;
using UnityEngine;

namespace FiniteStateMachine
{
    [Serializable]
    public abstract class BaseState : IState
    {

        public virtual string Name { get; protected set; }

        /**
         * Has to be called with the Update method of a StateMachine
         */
        public virtual void Update() { 
        
        }

        /**
         * Has to be called with the FixedUpdate method of a StateMachine
         */
        public virtual void FixedUpdate() { 
        
        }

        /**
         * Called when the state is exited
         */
        public virtual void OnExit() {
        
        }

        /**
         * Called when the state is entered
         */
        public virtual void OnEnter()
        {

        }

    }

}
