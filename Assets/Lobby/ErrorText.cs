using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Com.LarkinTuckerLLC.Pong
{
    [RequireComponent(typeof(Text))]
    public class ErrorText: MonoBehaviour
    {
        #region Private Variables
        Text _text;
        bool _playingErrored = PlayingErrored.InitialState;
        IDisposable _subscription;
        #endregion

        #region MonoBehaviour CallBacks
        void Start()
        {
            _text = this.GetComponent<Text>();
            _text.enabled = _playingErrored;
            _subscription = Provider.Store.Subscribe(state =>
            {
                bool nextPlayingErrored = PlayingErrored.Get(state);
                if (nextPlayingErrored == _playingErrored) {
                    return;
                }
                _playingErrored = nextPlayingErrored;
                _text.enabled = _playingErrored;
            });
        }

        void OnDestroy()
        {
            _subscription.Dispose();
        }
        #endregion
    }
}
