namespace Com.LarkinTuckerLLC.Pong
{
    public class Playing: Singleton<Playing>
    {
        public static bool InitialState = false;

        public static bool Reducer(bool state, Action action)
        {
            switch (action.Type)
            {
                case Provider.Actions.PLAYING_SET:
                    return action.PayloadBool;
                default:
                    return state;
            }
        }

        public static bool Get(State state)
        {
            return state.Playing;
        }

        protected Playing() { }

        public Action TryStart()
        {
            return new Action((dispatch, getState) =>
            {
                State state = getState();
                string playerName = Name.Get(state);
                PhotonNetwork.playerName = playerName;
                dispatch(PlayingRequested.Instance.Do());
                if (PhotonNetwork.connected)
                {
                    PhotonNetwork.JoinRandomRoom();
                }
                else
                {
                    PhotonNetwork.ConnectUsingSettings(Launcher.GAME_VERSION);
                }
            });
        }

        public Action SuccessStart()
        {
            return new Action((dispatch, getState) =>
            {
                var playerCount = PhotonNetwork.room.PlayerCount;
                dispatch(PlayerCount.Instance.Set(playerCount));
                dispatch(Set(true));
                PhotonNetwork.LoadLevel("Table");
            });
        }

        public Action ErroredStart()
        {
            return new Action((dispatch, getState) =>
            {
                dispatch(PlayingErrored.Instance.Do());
            });
        }

        public Action TryStop()
        {
            return new Action((dispatch, getState) =>
            {
                dispatch(PlayingRequested.Instance.Do());
                PhotonNetwork.LeaveRoom();
            });
        }

        public Action SuccessStop()
        {
            return new Action((dispatch, getState) =>
            {
                dispatch(PlayerCount.Instance.Set(0));
                dispatch(Set(false));
                PhotonNetwork.LoadLevel("Launcher");
            });
        }


        public Action Set(bool playing)
        {
            return new Action(Provider.Actions.PLAYING_SET, playing);
        }
    }
}
