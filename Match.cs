namespace MatchThree
{
    public struct Match
    {
        public int OrbCount;
        public OrbColor OrbType;
        public MatchTypes MatchType;
    }

    public enum MatchTypes
    {
        None,
        Three,
        Four,
        Row,
        Column,
        Linked,
        Cross
    }

    public static class MatchTypesHelper
    {
        public static readonly MatchTypes[] ValidColors = new MatchTypes[]
        {
            MatchTypes.Three, MatchTypes.Four, MatchTypes.Row,
            MatchTypes.Linked, MatchTypes.Cross
        };
    }
}