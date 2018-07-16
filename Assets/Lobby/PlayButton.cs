using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Com.LarkinTuckerLLC.Pong
{
    [RequireComponent(typeof(Button))]
    public class PlayButton : MonoBehaviour
    {
        #region Private Variables
        Button _button;
        string _name = Name.InitialState;
        bool _playingRequested = PlayingRequested.InitialState;
        IDisposable _subscription;
        #endregion

        #region MonoBehaviour CallBacks
        void Start()
        {
            _button = this.GetComponent<Button>();
            _button.interactable = _name != "";
            _subscription = Provider.Store.Subscribe(state =>
            {
                string nextName = Name.Get(state);
                bool nextPlayingRequested = PlayingRequested.Get(state);
                if (nextName == _name && nextPlayingRequested == _playingRequested)
                {
                    return;
                }
                _name = nextName;
                _playingRequested = nextPlayingRequested;
                _button.interactable = _name != "" && !_playingRequested;
            });
        }

		void OnDestroy()
		{
            _subscription.Dispose();	
		}
		#endregion

		#region Public Methods
		public void HandleClick()
        {
            Provider.Dispatch(Playing.Instance.TryStart());
        }
        #endregion
    }
}