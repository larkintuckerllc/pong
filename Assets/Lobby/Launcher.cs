using Photon;
using System;
using UniRx;

namespace Com.LarkinTuckerLLC.Pong
{
    public class Launcher : PunBehaviour
    {
        #region Private Static Variables
        static byte MAX_PLAYERS = 2;
        #endregion

        #region Public Static Variables
        public static string GAME_VERSION = "1";
        #endregion

        #region Private Variables
        bool _playingRequested;
        IDisposable _subscription;
        #endregion

        #region MonoBehaviour CallBacks
        void Start()
        {
            PhotonNetwork.autoJoinLobby = false;
            _subscription = Provider.Store.Subscribe(state =>
            {
                bool nextPlayingRequested = PlayingRequested.Get(state);
                if (nextPlayingRequested == _playingRequested)
                {
                    return;
                }
                _playingRequested = nextPlayingRequested;
            });
        }

        void OnDestroy()
        {
            _subscription.Dispose();
        }
		#endregion

		#region Photon.PunBehaviour CallBacks
		public override void OnFailedToConnectToPhoton(DisconnectCause cause)
		{
            Provider.Dispatch(Playing.Instance.ErroredStart());
		}
		public override void OnConnectedToMaster()
        {
            if (!_playingRequested)
            {
                return;
            }
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnectedFromPhoton()
        {
            Provider.Dispatch(Playing.Instance.Set(false));
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MAX_PLAYERS }, null);
        }

        public override void OnJoinedRoom()
        {
            Provider.Dispatch(Playing.Instance.SuccessStart());
        }
        #endregion
    }
}