using FiniteStateMachine;
using System;
using UnityEngine;

namespace Squares
{
    [Serializable]
    public class ButtonPressTransitionDefinition : TransitionDefinition
    {
        [SerializeField] private KeyCode _buttonName;
        [SerializeField] private ButtonPressPredicate.ButtonPressType _pressType;

        public ButtonPressTransitionDefinition()
        {

        }

        public ButtonPressTransitionDefinition(KeyCode buttonName, ButtonPressPredicate.ButtonPressType pressType)
        {
            _buttonName = buttonName;
            _pressType = pressType;
        }

        public override IPredicate GetPredicate()
        {
            return new ButtonPressPredicate(_buttonName, _pressType);
        }
    }


}
