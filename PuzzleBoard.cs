namespace MatchThree
{
    public class PuzzleBoard
    {
        private PuzzleOrb[,] _boardArray = new PuzzleOrb[0, 0];
        public int Rows { get { return _boardArray.GetLength(0); } }
        public int Columns { get { return _boardArray.GetLength(1); } }

        public PuzzleBoard(int rows, int colums)
        {
            _boardArray = new PuzzleOrb[rows, colums];
        }

        public PuzzleOrb this[int y, int x]
        {
            get { return _boardArray[y,x]; }
            set { _boardArray[y,x] = value; }
        }

        public PuzzleBoard CreateCopy()
        {
            PuzzleBoard newBoard = new PuzzleBoard(Rows, Columns);
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    newBoard[y, x] = new PuzzleOrb(x, y, _boardArray[y, x].orbColor);
                }
            }
            return newBoard;
        }
    }
}