using System;
using UnityEngine;

namespace Com.LarkinTuckerLLC.Pong
{
    public class Action
    {
        public Provider.Actions Type { get; private set; }

        public int PayloadInt { get; private set; }
        public bool PayloadBool { get; private set; }
        public string PayloadString { get; private set; }

        public Action(Provider.Actions type)
        {
            this.Type = type;
        }

        public Action(Provider.Actions type, int payload)
        {
            this.Type = type;
            this.PayloadInt = payload;
        }

        public Action(Provider.Actions type, bool payload)
        {
            this.Type = type;
            this.PayloadBool = payload;
        }

        public Action(Provider.Actions type, string payload)
        {
            this.Type = type;
            this.PayloadString = payload;
        }

        public Action(Action<Action<Action>> function)
        {
            this.Type = Provider.Actions.THUNK;
            function(Provider.Dispatch);
        }

        public Action(Action<Action<Action>, Func<State>> function)
        {
            this.Type = Provider.Actions.THUNK;
            function(Provider.Dispatch, Provider.GetState);
        }

    }
}
