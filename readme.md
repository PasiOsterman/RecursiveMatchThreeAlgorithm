# Recursive Match Three algorithm. (.Net C#)
Algorithm traverses board using recursion avoiding checking same orb twice. It is also able to differentiate matches such as three, four, row, column, linked and cross. 

Code is written to work without any game engine or framework so it should work with both Unity3D and XNA. It should also be relatively easy to reference and port to other object-oriented languages.

Code doesn't contain methods to move orbs as they're largely dependent on the platform and game in question, graphical visualization of orbs is also out of scope of the project. However it should not be too difficult to bind visual repesentation of orb to __PuzzleOrb__ and draw it to the screen based on it's index. 

## Usage
Create new __PuzzleBoard__ and __PuzzleBoardController__. Fille the board with random orbs using __FillBoardWithNewOrbs__ method. Check matches using __CheckForMatches__ and combos from those matches using __CheckForCombos__ method. 

Instead of destroying matched orbs, re-use them by moving them to the top using __MoveMatchedOrbsToTheTop__ method and assign them new color with __AssignNewColorToMatchedOrbs__. In game one can visualize skyfalls by tweening the visual representation of the orbs from above the board to their new places. 

```csharp

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


```