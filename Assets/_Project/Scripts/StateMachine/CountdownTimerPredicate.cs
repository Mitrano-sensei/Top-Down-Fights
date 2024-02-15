using Utilities;

namespace FiniteStateMachine
{
    public class CountdownTimerPredicate : IPredicate
    {
        private CountdownTimer _countdownTimer;

        public CountdownTimerPredicate(CountdownTimer countdownTimer)
        {
            _countdownTimer = countdownTimer;
        }

        public bool Evaluate()
        {
            return _countdownTimer.IsFinished;
        }
    }
}
