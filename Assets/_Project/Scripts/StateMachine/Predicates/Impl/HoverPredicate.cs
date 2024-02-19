using Utilities;

namespace FiniteStateMachine
{
    public class HoverPredicate : IPredicate
    {
        private Hoverable2D _hoverable;

        public HoverPredicate(Hoverable2D hoverable)
        {
            _hoverable = hoverable;
        }

        public bool Evaluate() => _hoverable.IsHovered;
    }
}
