using System;
using UnityEngine;

namespace FiniteStateMachine
{
    public class ButtonPressPredicate : IPredicate
    {
        private KeyCode _key;
        private ButtonPressType _pressPredicate;

        public ButtonPressPredicate(KeyCode key, ButtonPressType pressPredicate)
        {
            _key = key;
            _pressPredicate = pressPredicate;
        }

        public bool Evaluate()
        {
            switch (_pressPredicate)
            {
                case ButtonPressType.Down:
                    return Input.GetKeyDown(_key);
                case ButtonPressType.Up:
                    return Input.GetKeyUp(_key);
                case ButtonPressType.Any:
                    return Input.GetKey(_key);
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        public enum ButtonPressType
        {
            Down,
            Up,
            Any
        }

    }
}
