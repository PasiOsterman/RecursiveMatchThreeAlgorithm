namespace MatchThree
{
    public static class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("Creating random board.");
            PuzzleBoard board = new PuzzleBoard(5,5);
            PuzzleBoardController boardController = new PuzzleBoardController(board);
            boardController.FillBoardWithNewOrbs();
            System.Console.WriteLine(boardController.GetBoardString());

            System.Console.WriteLine("Checking matches");
            boardController.CheckForMatches();
            boardController.CheckForCombos();
            System.Console.WriteLine(boardController.GetCombosString());

            if(boardController.ComboCount > 0)
            {
                System.Console.WriteLine("Moving matched orbs to the top");
                boardController.MoveMatchedOrbsToTheTop();
                System.Console.WriteLine(boardController.GetBoardString());

                System.Console.WriteLine("Assign new color to matched orbs");
                boardController.AssignNewColorToMatchedOrbs();
                boardController.ClearMatches();
                System.Console.WriteLine(boardController.GetBoardString());
            }
        }
    }
}