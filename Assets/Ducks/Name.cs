namespace Com.LarkinTuckerLLC.Pong
{
    public class Name : Singleton<Name>
    {
        public static string InitialState = "";

        public static string Reducer(string state, Action action)
        {
            switch (action.Type)
            {
                case Provider.Actions.NAME_SET:
                    return action.PayloadString;
                default:
                    return state;
            }
        }

        public static string Get(State state)
        {
            return state.Name;
        }

        protected Name() { }

        public Action Set(string name)
        {
            return new Action(Provider.Actions.NAME_SET, name);
        }
    }
}
