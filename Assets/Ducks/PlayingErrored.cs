namespace Com.LarkinTuckerLLC.Pong
{
    public class PlayingErrored : Singleton<PlayingErrored>
    {
        public static bool InitialState = false;

        public static bool Reducer(bool state, Action action)
        {
            switch (action.Type)
            {
                case Provider.Actions.PLAYING_ERRORED:
                    return true;
                case Provider.Actions.PLAYING_REQUESTED:
                    return false;
                case Provider.Actions.PLAYING_SET:
                    if (action.PayloadBool == true) {
                        return false;
                    }
                    return state;
                default:
                    return state;
            }
        }

        public static bool Get(State state)
        {
            return state.PlayingErrored;
        }

        protected PlayingErrored() { }

        public Action Do()
        {
            return new Action(Provider.Actions.PLAYING_ERRORED);
        }
    }
}
