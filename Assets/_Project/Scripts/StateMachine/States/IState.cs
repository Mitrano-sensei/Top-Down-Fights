using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public interface IState
    {
        string Name { get; }

        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
    }

}
