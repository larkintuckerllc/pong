using Photon;

namespace Com.LarkinTuckerLLC.Pong
{
    public class GameManager : PunBehaviour
    {
        #region Photon.PunBehaviour CallBacks
        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            Provider.Dispatch(PlayerCount.Instance.Set(2));
        }

		public override void OnLeftRoom()
		{
            Provider.Dispatch(Playing.Instance.SuccessStop());
		}

		public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            Provider.Dispatch(Playing.Instance.TryStop());
        }
        #endregion
    }
}
