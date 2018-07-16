using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Com.LarkinTuckerLLC.Pong
{
    [RequireComponent(typeof(Text))]
    public class WaitingText : MonoBehaviour
    {
        #region Private Variables
        Text _text;
        int _playerCount = PlayerCount.InitialState;
        IDisposable _subscription;
        #endregion

        #region MonoBehaviour CallBacks
        void Start()
        {
            _text = this.GetComponent<Text>();
            _text.enabled = _playerCount == 1;
            _subscription = Provider.Store.Subscribe(state =>
            {
                int nextPlayerCount = PlayerCount.Get(state);
                if (nextPlayerCount == _playerCount)
                {
                    return;
                }
                _playerCount = nextPlayerCount;
                _text.enabled = _playerCount == 1;
            });
        }

        void OnDestroy()
        {
            _subscription.Dispose();
        }
        #endregion
    }
}
