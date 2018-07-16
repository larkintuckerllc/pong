using UnityEngine;
using UnityEngine.UI;

namespace Com.LarkinTuckerLLC.Pong
{
    [RequireComponent(typeof(Button))]
    public class StopButton : MonoBehaviour
    {
        #region Public Methods
        public void HandleClick()
        {
            Provider.Dispatch(Playing.Instance.TryStop());
        }
        #endregion
    }
}
