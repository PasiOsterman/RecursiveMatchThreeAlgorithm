
namespace MatchThree
{
    public abstract class PuzzleBoardEventArgs : System.EventArgs{}

    public class PuzzleBoardChangedEventArgs : PuzzleBoardEventArgs
    {
        public PuzzleBoardChangedEventArgs(PuzzleOrb[] orbs) { Orbs = orbs; }
        public PuzzleOrb[] Orbs { get; }
    }

    public class BoardWasFilledWithNewOrbs : PuzzleBoardChangedEventArgs
    {
        public BoardWasFilledWithNewOrbs(PuzzleOrb[] orbs) : base(orbs){}
    }

    public class BoardOrbColorsChanged : PuzzleBoardChangedEventArgs
    {
        public BoardOrbColorsChanged(PuzzleOrb[] orbs) : base(orbs){ }
    }
}
