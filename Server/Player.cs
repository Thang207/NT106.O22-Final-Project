namespace Server
{
    struct Player
    {
        //User instance
        public User user;
        //whether the game has started
        public bool started;
        // is there anyone sitting down
        public bool someone;
        // whether the game has stop
        public bool stopped;
    }
}
