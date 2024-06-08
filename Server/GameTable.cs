namespace Server
{
    class GameTable
    {
        // A game has 2 seat - 2 player 
        public Player[] gamePlayer;
        public GameTable()
        {
            gamePlayer = new Player[2];
        }
    }
}
