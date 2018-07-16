using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Com.LarkinTuckerLLC.Pong
{
    [RequireComponent(typeof(InputField))]
    public class NameInput : MonoBehaviour
    {
        #region Private Variables
        InputField _inputField;
        bool _playingRequested = PlayingRequested.InitialState;
        IDisposable _subscription;
        #endregion

        #region MonoBehaviour CallBacks
        void Start()
        {
            _inputField = this.GetComponent<InputField>();
            _subscription = Provider.Store.Subscribe(state =>
            {
                // INITIALIZE INPUT
                string value = _inputField.text;
                string playerName = Name.Get(state);
                if (value == "" && playerName != "")
                {
                    _inputField.text = playerName; 
                }

                // CHECK FOR PLAYING REQUESTED
                bool nextPlayingRequested = PlayingRequested.Get(state);
                if (nextPlayingRequested == _playingRequested)
                {
                    return;
                }
                _playingRequested = nextPlayingRequested;
                _inputField.interactable = !_playingRequested;
            });
        }

		private void OnDestroy()
		{
            _subscription.Dispose();
		}
		#endregion

		#region Public Methods
		public void HandleValueChanged(string value)
        {
            Provider.Dispatch(Name.Instance.Set(value));
        }
        #endregion
    }
}
