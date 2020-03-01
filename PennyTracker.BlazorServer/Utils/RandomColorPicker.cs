using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PennyTracker.BlazorServer.Utils
{
    public static class RandomKnownColorPicker
    {
        private static Random randomGenerator;
        private static List<KnownColor> knownColors;
        static RandomKnownColorPicker()
        {
            randomGenerator = new Random();
            knownColors = Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>().ToList();
        }

        public static KnownColor Get() => knownColors[randomGenerator.Next(knownColors.Count)];
    }
}
