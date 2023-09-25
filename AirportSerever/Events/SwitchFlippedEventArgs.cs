using AirportSerever.Enums;

namespace AirportSerever.Events
{
    public class SwitchFlippedEventArgs : EventArgs
    {
        private SwitchState _state;
        public SwitchFlippedEventArgs(SwitchState state)
        {
            _state = state;
        }

        public SwitchState State
        {
            get { return _state; }
        }

       
    }
}
