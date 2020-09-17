using System;

namespace MatchThree
{
    public static class OrbColorHelper
    {
        public static readonly OrbColor[] ValidOrbColors = new OrbColor[]
        {
            OrbColor.Blue, OrbColor.Green, OrbColor.Red,
            OrbColor.Yellow, OrbColor.Purple, OrbColor.Magenta
        };

        private static readonly Random _rand = new Random();
        public static OrbColor GetRandomOrbColor()
        {
            return ValidOrbColors[_rand.Next(ValidOrbColors.Length)];
        }

        private static string[] _orbLetters;
        public static string[] ColorLetters
        {
            get
            {
                if (_orbLetters == null)
                {
                    _orbLetters = new string[ColorNames.Length];
                    for (int i = 0; i < _orbLetters.Length; i++)
                        _orbLetters[i] = ColorNames[i].Substring(0, 1);
                }
                return _orbLetters;
            }
        }

        private static string[] _colorNames;
        public static string[] ColorNames
        {
            get
            {
                if (_colorNames == null)
                    _colorNames = Enum.GetNames(typeof(OrbColor)) as string[];

                return _colorNames;
            }
        }

        public static string GetColoredOrbLetter(OrbColor orbType)
        {
            int index = (int)orbType;
            string letterStr = ColorLetters[index];
            return letterStr;
        }

        public static string GetColoredOrbName(OrbColor orbType)
        {
            int index = (int)orbType;
            string colorStr = ColorNames[index];

            return colorStr;
        }
    }
}