using FiniteStateMachine;
using System;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Squares
{
    [Serializable]
    public abstract class TransitionDefinition
    {
        private GameObject _gameObject;
        public GameObject GameObject => _gameObject;

        public void SetGameObject(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

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
        [Tooltip("If set to null, will take the one attached on the gameobject of this component.")]
        [SerializeField] private Hoverable2D _hoverable;
        
        public override IPredicate GetPredicate()
        {
            _hoverable = _hoverable ?? GameObject.GetComponent<Hoverable2D>();
            if (_hoverable == null) Debug.LogError("No Hoverable found on " + GameObject.name);

            var multiTransition = new MultiTransitionDefinition();

            multiTransition.Add(new HoverTransitionDefinition(_hoverable));
            multiTransition.Add(new ButtonPressTransitionDefinition(UnityEngine.KeyCode.Mouse0, ButtonPressPredicate.ButtonPressType.Down));

            return multiTransition.GetPredicate();
        }
    }
}
