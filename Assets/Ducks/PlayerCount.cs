namespace Com.LarkinTuckerLLC.Pong
{
    public class PlayerCount: Singleton<PlayerCount>
    {
        public static int InitialState = 0;

        public static int Reducer(int state, Action action)
        {
            switch (action.Type)
            {
                case Provider.Actions.PLAYER_COUNT_SET:
                    return action.PayloadInt;
                default:
                    return state;
            }
        }

        public static int Get(State state)
        {
            return state.PlayerCount;
        }

        protected PlayerCount() { }

        public Action Set(int playerCount)
        {
            return new Action(Provider.Actions.PLAYER_COUNT_SET, playerCount);
        }
    }
}
