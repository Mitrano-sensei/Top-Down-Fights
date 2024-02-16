using FiniteStateMachine;
using System;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Square
{
    [Serializable]
    public abstract class TransitionDefinition
    {
        public abstract IPredicate GetPredicate();
        public virtual Action GetOnExitAction()
        {
            return null;
        }

        public virtual Action GetOnEnterAction()
        {
            return null;
        }

        public virtual UnityAction<float> GetOnUpdateAction()
        {
            return null;
        }

        public virtual UnityAction<float> GetOnFixedUpdateAction()
        {
            return null;
        }
    }

    [Serializable]
    public class ClickTransitionDefinition : TransitionDefinition
    {
        [SerializeField] private Hoverable2D _hoverable;

        
        public override IPredicate GetPredicate()
        {
            var multiTransition = new MultiTransitionDefinition();

            multiTransition.Add(new HoverTransitionDefinition(_hoverable));
            multiTransition.Add(new ButtonPressTransitionDefinition(UnityEngine.KeyCode.Mouse0, ButtonPressPredicate.ButtonPressType.Down));

            return multiTransition.GetPredicate();
        }
    }
}
