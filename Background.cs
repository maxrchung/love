using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.CommandValues;
using System.Collections.Generic;

namespace StorybrewScripts
{
    public class Timing
    {
        public float startTime { get; set; }
        public float endTime { get; set; }
        public CommandColor startColor { get; set; }
        public CommandColor endColor { get; set; }

        public static Timing Instant(float time, CommandColor color)
        {
            var timing = new Timing()
            {
                startTime = time,
                endTime = time,
                startColor = color,
                endColor = color,
            };
            return timing;
        }

        public static Timing Transition(float startTime, float endTime, CommandColor startColor, CommandColor endColor)
        {
            var timing = new Timing()
            {
                startTime = startTime,
                endTime = endTime,
                startColor = startColor,
                endColor = endColor,
            };
            return timing;
        }
    }

    public static class Colors
    {
        private static CommandColor Hsb(double hue, double saturation, double brightness)
        {
            return CommandColor.FromHsb(hue, saturation, brightness);
        }

        public static CommandColor MonogatariWhite = Hsb(252, 0.2, 0.99);
        public static CommandColor MonogatariRed = Hsb(7.43, 0.98, 0.83);
        public static CommandColor MonogatariBlue = Hsb(205, 0.8, 0.67);
        public static CommandColor MonogatariOrange = Hsb(37, 0.54, 0.96);
        public static CommandColor MonogatariBrown = Hsb(46, 0.18, 0.66);
        public static CommandColor MonogatariGreen = Hsb(86, 0.26, 0.26);
        public static CommandColor MonogatariGrey = Hsb(235, 0.3, 0.5);
        public static CommandColor MonogatariNight = Hsb(235, 0.45, 0.35);
        public static CommandColor MonogatariBlack = Hsb(32, 0.47, 0.23);
    }

    public class Background : StoryboardObjectGenerator
    {
        private List<Timing> timings = new List<Timing>()
        {
            Timing.Instant(0, Colors.MonogatariNight),
            Timing.Instant(169, Colors.MonogatariRed),
            Timing.Instant(407, Colors.MonogatariBlue),
            Timing.Instant(586, Colors.MonogatariOrange),
            Timing.Instant(705, Colors.MonogatariBrown),
            Timing.Instant(824, Colors.MonogatariGreen),
            Timing.Instant(943, Colors.MonogatariBlack),
            Timing.Instant(1062, Colors.MonogatariRed),
            Timing.Instant(1538, Colors.MonogatariBlue),

        };

        public override void Generate()
        {
            var layer = GetLayer("Background");

            // Add some bleed to sides
            var bg = layer.CreateSprite("w.png", OsbOrigin.TopLeft, new Vector2(-108, -1));
            bg.ScaleVec(0, new Vector2(856, 482));
            bg.Color(0, 0.2, 0.2, 0.2);
            foreach (var timing in timings)
            {
                bg.Color(timing.startTime, timing.endTime, timing.startColor, timing.endColor);
            }

            bg.Fade(170000, 170000, 1, 0);
        }
    }
}
