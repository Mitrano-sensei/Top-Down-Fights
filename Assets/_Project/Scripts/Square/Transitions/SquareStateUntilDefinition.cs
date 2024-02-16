using Squares;
using System;
using UnityEngine;

namespace Square
{
    [Serializable]
    public class SquareStateUntilDefinition
    {
        [SerializeReference, SubclassSelector] public SquareState SquareState;
        [SerializeReference, SubclassSelector] public TransitionDefinition Until;
    }

    [Serializable]
    public class  SquareStateAnyDefinition
    {
        [SerializeField] public string To;
        [SerializeReference, SubclassSelector] public TransitionDefinition When;
    }


}
