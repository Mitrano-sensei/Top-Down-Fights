using FiniteStateMachine;
using System;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Square
{
    [Serializable]
    public class CountDownTransitionDefinition : TransitionDefinition
    {
        [SerializeField] private float _duration;

        private CountdownTimer Timer { get; set; }

        public override IPredicate GetPredicate()
        {
            Timer = new CountdownTimer(_duration);

            return new CountdownTimerPredicate(Timer);
        }

        public override Action GetOnEnterAction()
        {
            return () => Timer.Start();
        }

        public override UnityAction<float> GetOnUpdateAction()
        {
            return Timer.Tick;
        }
    }


}
