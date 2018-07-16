using UnityEngine;

namespace Com.LarkinTuckerLLC.Pong
{
    public class Initialize : MonoBehaviour
    {
        #region MonoBehaviour CallBacks
        void Awake()
        {
            Provider.Initialize();
        }
        #endregion
    }
}
