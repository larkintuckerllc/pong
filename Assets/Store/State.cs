namespace Com.LarkinTuckerLLC.Pong
{
    public class State
    {
        public string Name { get; private set; }
        public int PlayerCount{ get; private set; }
        public bool Playing { get; private set; }
        public bool PlayingErrored { get; private set;  }
        public bool PlayingRequested { get; private set; }

        public State(string name, int playerCount, bool playing, bool playingErrored, bool playingRequested)
        {
            Name = name;
            PlayerCount = playerCount;
            Playing = playing;
            PlayingErrored = playingErrored;
            PlayingRequested = playingRequested;
        }
    }
}
