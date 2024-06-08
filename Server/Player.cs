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
        // Store score of player 
        public int score;
    }
}
