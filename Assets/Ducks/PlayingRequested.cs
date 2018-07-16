namespace Com.LarkinTuckerLLC.Pong
{
    public class PlayingRequested : Singleton<PlayingRequested>
    {
        public static bool InitialState = false;

        public static bool Reducer(bool state, Action action)
        {
            switch (action.Type)
            {
                case Provider.Actions.PLAYING_REQUESTED:
                    return true;
                case Provider.Actions.PLAYING_SET:
                    return false;
                case Provider.Actions.PLAYING_ERRORED:
                    return false;
                default:
                    return state;
            }
        }

        public static bool Get(State state)
        {
            return state.PlayingRequested;
        }

        protected PlayingRequested() { }

        public Action Do()
        {
            return new Action(Provider.Actions.PLAYING_REQUESTED);
        }
    }
}
