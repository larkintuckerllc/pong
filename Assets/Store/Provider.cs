using System;
using UnityEngine;
using UniRx;

namespace Com.LarkinTuckerLLC.Pong
{
    public static class Provider
    {
        public enum Actions
        {
            __INIT,
            NAME_SET,
            PLAYER_COUNT_SET,
            PLAYING_ERRORED,
            PLAYING_REQUESTED,
            PLAYING_SET,
            THUNK,
        }

        static State state;

        static bool initialized = false;

        public static State GetState()
        {
            return state;
        }

        public static State Reducer(State state, Action action)
        {
            bool hasChanged = false;
            string nextStateName = Name.Reducer(state.Name, action);
            if (nextStateName != state.Name)
            {
                hasChanged = true;
            }
            int nextStatePlayerCount = PlayerCount.Reducer(state.PlayerCount, action);
            if (nextStatePlayerCount != state.PlayerCount)
            {
                hasChanged = true;
            }
            bool nextStatePlaying = Playing.Reducer(state.Playing, action);
            if (nextStatePlaying != state.Playing)
            {
                hasChanged = true;
            }
            bool nextStatePlayingErrored = PlayingErrored.Reducer(state.PlayingErrored, action);
            if (nextStatePlayingErrored != state.PlayingErrored)
            {
                hasChanged = true;
            }
            bool nextStatePlayingRequested = PlayingRequested.Reducer(state.PlayingRequested, action);
            if (nextStatePlayingRequested != state.PlayingRequested)
            {
                hasChanged = true;
            }
            return hasChanged ? new State(nextStateName, nextStatePlayerCount, nextStatePlaying, nextStatePlayingErrored, nextStatePlayingRequested) : state;
        }

        public static Action Logger(Action action)
        {
            Debug.Log(action.Type);
            return action;
        }

        public static Boolean FilterThunk(Action action)
        {
            return action.Type != Actions.THUNK;
        }

        public static State ExtractState(State state)
        {
            Provider.state = state;
            return state;
        }

        static ISubject<Action> StoreDispatch;

        public static void Dispatch(Action action)
        {
            StoreDispatch.OnNext(action);
        }

        public static BehaviorSubject<State> Store { get; private set; }

        public static void Initialize()
        {
            if (initialized)
            {
                return;
            }
            initialized = true;
            StoreDispatch = new Subject<Action>();
            State initialState = new State(
                Name.InitialState,
                PlayerCount.InitialState,
                Playing.InitialState,
                PlayingErrored.InitialState,
                PlayingRequested.InitialState
            );
            Store = new BehaviorSubject<State>(initialState);
            StoreDispatch
                .Where<Action>(FilterThunk)
                .Select(Logger)
                .Scan(initialState, Reducer)
                .Select(ExtractState)
                .Subscribe(Store);
            Dispatch(new Action(Actions.__INIT));
        }
    }
}