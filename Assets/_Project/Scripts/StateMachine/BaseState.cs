using UnityEngine;

namespace FiniteStateMachine
{
    public abstract class BaseState : IState
    {

        
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
