
namespace MatchThree
{
    public class PuzzleOrb
    {
        public OrbColor orbColor = OrbColor.None;
        public Index2D index = Index2D.Zero;
        public PuzzleOrb() { }

        public PuzzleOrb(int inX, int inY, OrbColor inOrbType)
        {
            index.x = inX;
            index.y = inY;
            orbColor = inOrbType;
        }

        public bool HasMatchingColor(PuzzleOrb puzzleOrb)
        {
            if (!GetOrbTypeValidity() || !puzzleOrb.GetOrbTypeValidity())
                return false;

            return HasMatchingColor(puzzleOrb.orbColor);
        }

        public bool HasMatchingColor(OrbColor matchColor)
        {
            return orbColor == matchColor;
        }

        public bool GetOrbTypeValidity()
        {
            if (orbColor != OrbColor.None)
                return true;
            else
                return false;
            
        }
    } 
}