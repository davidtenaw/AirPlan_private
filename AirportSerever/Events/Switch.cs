using AirportSerever.Enums;

namespace AirportSerever.Events
{
    public delegate void SwitchFlippedEventHandler(object sender,
        SwitchFlippedEventArgs args);
    public class Switch
    {

        private SwitchState _state = SwitchState.Off;
        public SwitchState State
        {
            get { return _state; }
        }

        public void Flip()
        {
            if (_state == SwitchState.Off)
            {
                _state = SwitchState.On;
            }
            else
            {
                _state = SwitchState.Off;
            }
            // We call the protected method that raises the event.
            RaiseSwitchFlipped(new SwitchFlippedEventArgs(_state));
        }

        protected virtual void RaiseSwitchFlipped(SwitchFlippedEventArgs args)
        {
            // We must check that the event is not null.  It is null
            //  if no one registered to the event.
            //
            if (SwitchFlipped != null)
            {
                SwitchFlipped.Invoke(this, args);
            }
        }

        // This is the definition of the event.  Note that the delegate
        //  appears in the event definition, so only methods that match
        //  the delegate signature can register to this event.
        //
        public event SwitchFlippedEventHandler SwitchFlipped;

    }
}
