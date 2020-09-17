using System.Collections.Generic;

namespace MatchThree
{
    public class PuzzleBoardController
    {
        public int ComboCount { get { return Combos.Count; } }
        public List<List<PuzzleOrb>> AllMatches { get; } = new List<List<PuzzleOrb>>();
        public List<Match> Combos { get; } = new List<Match>();
        public PuzzleBoard Board { get; }
        
        private List<PuzzleOrb> Traversed { get; } = new List<PuzzleOrb>();

        public PuzzleBoardController(PuzzleBoard board)
        {
            Board = board;
        }

        public void FillBoardWithNewOrbs()
        {
            for (int y = 0; y < Board.Rows; y++)
            {
                for (int x = 0; x < Board.Columns; x++)
                {
                    PuzzleOrb newOrb = new PuzzleOrb(x, y, OrbColorHelper.GetRandomOrbColor());
                    Board[y, x] = newOrb;
                }
            }
        }

        public void SwapOrbPlaces(PuzzleOrb firstOrb, PuzzleOrb secondOrb)
        {
            SwapOrbPlaces(firstOrb.index, secondOrb.index);
        }

        public void SwapOrbPlaces(Index2D from, Index2D to)
        {
            PuzzleOrb fromOrb = Board[from.y, from.x];
            PuzzleOrb toOrb = Board[to.y, to.x];

            fromOrb.index = to;
            toOrb.index = from;

            Board[to.y, to.x] = fromOrb;
            Board[from.y, from.x] = toOrb;
        }

        public void AssignNewColorToMatchedOrbs()
        {
            List<PuzzleOrb> matched = GetAllMatchedOrbs();
            for (int i = 0; i < matched.Count; i++)
                matched[i].orbColor = OrbColorHelper.GetRandomOrbColor();
        }

        public void MoveMatchedOrbsToTheTop()
        {
            List<PuzzleOrb> matched = GetAllMatchedOrbs();

            for (int y = 0; y < Board.Rows; y++)
            {
                for (int x = 0; x < Board.Columns; x++)
                {
                    PuzzleOrb it = GetOrbWithIndex(new Index2D(x, y));
                    if (!matched.Contains(it))
                        continue;

                    for (int h = y + 1; h < Board.Rows; h++)
                    {
                        PuzzleOrb it2 = GetOrbWithIndex(new Index2D(x, h));
                        if (!matched.Contains(it2))
                        {
                            SwapOrbPlaces(it, it2);
                            int index = matched.IndexOf(it);
                        }
                    }
                }
            }
        }

        public void CheckForCombos()
        {
            for (int i = 0; i < AllMatches.Count; i++)
            {
                Match newMatch = new Match
                {
                    MatchType = GetMatchType(AllMatches[i]),
                    OrbCount = AllMatches[i].Count,
                    OrbType = AllMatches[i][0].orbColor
                };
                Combos.Add(newMatch);
            }
        }

        public void CheckForMatches()
        {
            Traversed.Clear();
            AllMatches.Clear();

            for (int y = 0; y < Board.Rows; y++)
            {
                for (int x = 0; x < Board.Columns; x++)
                {
                    PuzzleOrb orb = GetOrbWithIndex(new Index2D(x, y));
                    if (Traversed.Contains(orb))
                        continue;

                    List<List<PuzzleOrb>> matchGroup = new List<List<PuzzleOrb>>();
                    TraverseOrb(orb, matchGroup);

                    if (matchGroup.Count > 0)
                    {
                        List<PuzzleOrb> matchedOrbs = new List<PuzzleOrb>();
                        for (int i = 0; i < matchGroup.Count; i++)
                        {
                            for (int j = 0; j < matchGroup[i].Count; j++)
                            {
                                if (!matchedOrbs.Contains(matchGroup[i][j]))
                                    matchedOrbs.Add(matchGroup[i][j]);
                            }
                        }
                        AllMatches.Add(matchedOrbs);
                    }
                }
            }
        }

        public string GetCombosString()
        {
            string matchString = "";
            matchString += "Combo count: " + Combos.Count + "\n";

            for (int i = 0; i < Combos.Count; i++)
            {
                matchString += "[";
                matchString += "Combo#" + i;
                matchString += " OrbCount: " + Combos[i].OrbCount;
                matchString += " Orb Color: " + OrbColorHelper.GetColoredOrbName(Combos[i].OrbType);
                matchString += " Match Type: " + Combos[i].MatchType.ToString();
                matchString += "]\n";
            }
            return matchString;
        }

        public string GetBoardString()
        {
            string boardString = "\n";
            for (int y = 0; y < Board.Rows; y++)
            {
                for (int x = 0; x < Board.Columns; x++)
                {
                    boardString = "[" + OrbColorHelper.GetColoredOrbLetter(Board[y, x].orbColor) + "]" + boardString;
                }
                boardString = "\n" + boardString;
            }
            return boardString;
        }

        private PuzzleOrb GetOrbWithIndex(Index2D index)
        {
            if (index.x < 0 || index.x >= Board.Columns)
                return null;

            if (index.y < 0 || index.y >= Board.Rows)
                return null;

            return Board[index.y, index.x];
        }

        private List<PuzzleOrb> GetAllMatchedOrbs()
        {
            List<PuzzleOrb> matched = new List<PuzzleOrb>();
            for (int i = 0; i < AllMatches.Count; i++)
            {
                for (int j = 0; j < AllMatches[i].Count; j++)
                {
                    matched.Add(AllMatches[i][j]);
                }
            }
            return matched;
        }

        public void ClearMatches()
        {
            AllMatches.Clear();
        }
        
        public void ClearCombos()
        {
            Combos.Clear();
        }

        private MatchTypes GetMatchType(List<PuzzleOrb> matches)
        {
            int orbCount = matches.Count;
            if (orbCount == 3)
            {
                return MatchTypes.Three;
            }
            else if (orbCount == 4)
            {
                return MatchTypes.Four;
            }
            else
            {
                List<Index2D> matchIndexes = new List<Index2D>();
                for (int i = 0; i < matches.Count; i++)
                    matchIndexes.Add(matches[i].index);

                if (orbCount == 5)
                {
                    Index2D leftMostIndex = matchIndexes[0];
                    for (int i = 0; i < matchIndexes.Count; i++)
                    {
                        if (leftMostIndex.x > matchIndexes[i].x)
                            leftMostIndex = matchIndexes[i];
                    }

                    Index2D crossCenter = leftMostIndex.RightIndex;
                    Index2D[] arr = new Index2D[]
                    {
                        crossCenter,
                        crossCenter.RightIndex,
                        crossCenter.UpIndex,
                        crossCenter.DownIndex
                    };

                    bool hasIndexes = true;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (!matchIndexes.Contains(arr[i]))
                        {
                            hasIndexes = false;
                            break;
                        }
                    }

                    if (hasIndexes)
                        return MatchTypes.Cross;
                }

                if (orbCount == Board.Columns || orbCount == Board.Rows)
                {
                    bool inSameRow = true;
                    bool inSameColumn = true;
                    int y = matchIndexes[0].y;
                    int x = matchIndexes[0].x;

                    for (int j = 1; j < matchIndexes.Count; j++)
                    {
                        if (matchIndexes[j].y != y)
                            inSameRow = false;

                        if (matchIndexes[j].x != x)
                            inSameColumn = false;

                        if (!inSameColumn && !inSameRow)
                            break;
                    }

                    if (orbCount == Board.Columns && inSameRow)
                        return MatchTypes.Row;
                    else if (orbCount == Board.Rows && inSameColumn)
                        return MatchTypes.Column;
                    else
                        return MatchTypes.Linked;
                }
                else
                {
                    return MatchTypes.Linked;
                }
            }
        }

        private void TraverseOrb(PuzzleOrb traverseOrb, List<List<PuzzleOrb>> matchGroup)
        {
            List<PuzzleOrb> horizontal = new List<PuzzleOrb>() { traverseOrb };
            TraverseRight(traverseOrb, matchGroup, horizontal);
            TraverseLeft(traverseOrb, matchGroup, horizontal);

            List<PuzzleOrb> vertical = new List<PuzzleOrb>() { traverseOrb };
            TraverseUp(traverseOrb, matchGroup, vertical);
            TraverseDown(traverseOrb, matchGroup, vertical);

            //First orb should not be added to traversed list before traversal has been completed due to how it would cause problems with circular traversal.
            //I.E two columns of 3 red orbs would only clear the second column because 7th traversal cannot be completed due to 1th being already in tarversed list.
            //[5][4]
            //[6][3]
            //[1][2]

            if (!Traversed.Contains(traverseOrb))
                Traversed.Add(traverseOrb);
        }

        private void TraverseDown(PuzzleOrb orb, List<List<PuzzleOrb>> matchGroup, List<PuzzleOrb> match)
        {
            PuzzleOrb orbBelow = GetOrbWithIndex(orb.index.DownIndex);
            if (orbBelow != null && !HasOrbAlreadyBeenTraversed(orbBelow) && orb.HasMatchingColor(orbBelow))
            {
                match.Add(orbBelow);
                TraverseDown(orbBelow, matchGroup, match);
                Traversed.Add(orbBelow);

                List<PuzzleOrb> vertical = new List<PuzzleOrb>() { orbBelow };
                TraverseLeft(orbBelow, matchGroup, vertical);
                TraverseRight(orbBelow, matchGroup, vertical);
            }
            else
            {
                if (match.Count > 2 && !matchGroup.Contains(match))
                    matchGroup.Add(match);
            }
        }

        private void TraverseUp(PuzzleOrb orb, List<List<PuzzleOrb>> matchGroup, List<PuzzleOrb> match)
        {
            PuzzleOrb orbAbove = GetOrbWithIndex(orb.index.UpIndex);
            if (orbAbove != null && !HasOrbAlreadyBeenTraversed(orbAbove) && orb.HasMatchingColor(orbAbove))
            {
                match.Add(orbAbove);
                TraverseUp(orbAbove, matchGroup, match);
                Traversed.Add(orbAbove);

                List<PuzzleOrb> vertical = new List<PuzzleOrb>() { orbAbove };
                TraverseLeft(orbAbove, matchGroup, vertical);
                TraverseRight(orbAbove, matchGroup, vertical);
            }
            else
            {
                if (match.Count > 2 && !matchGroup.Contains(match))
                    matchGroup.Add(match);
            }
        }

        private void TraverseLeft(PuzzleOrb orb, List<List<PuzzleOrb>> matchGroup, List<PuzzleOrb> match)
        {
            PuzzleOrb orbLeft = GetOrbWithIndex(orb.index.LeftIndex);
            if (orbLeft != null && !HasOrbAlreadyBeenTraversed(orbLeft) && orb.HasMatchingColor(orbLeft))
            {
                match.Add(orbLeft);
                TraverseLeft(orbLeft, matchGroup, match);
                Traversed.Add(orbLeft);

                List<PuzzleOrb> vertical = new List<PuzzleOrb>() { orbLeft };
                TraverseUp(orbLeft, matchGroup, vertical);
                TraverseDown(orbLeft, matchGroup, vertical);
            }
            else
            {
                if (match.Count > 2 && !matchGroup.Contains(match))
                    matchGroup.Add(match);
            }
        }

        private void TraverseRight(PuzzleOrb orb, List<List<PuzzleOrb>> matchGroup, List<PuzzleOrb> match)
        {
            PuzzleOrb orbRight = GetOrbWithIndex(orb.index.RightIndex);
            if (orbRight != null && !HasOrbAlreadyBeenTraversed(orbRight) && orb.HasMatchingColor(orbRight))
            {
                match.Add(orbRight);
                TraverseRight(orbRight, matchGroup, match);
                Traversed.Add(orbRight);

                List<PuzzleOrb> vertical = new List<PuzzleOrb>() { orbRight };
                TraverseUp(orbRight, matchGroup, vertical);
                TraverseDown(orbRight, matchGroup, vertical);
            }
            else
            {
                if (match.Count > 2 && !matchGroup.Contains(match))
                    matchGroup.Add(match);

            }
        }

        private bool HasOrbAlreadyBeenTraversed(PuzzleOrb orb)
        {
            return Traversed.Contains(orb);
        }
    }
}