using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Collections.Generic;

namespace StorybrewScripts
{
    public class Background : StoryboardObjectGenerator
    {
        private List<Timing> timings = new List<Timing>()
        {
            Timing.Instant(0, Colors.Steel),
            Timing.Instant(169, Colors.Red),
            Timing.Instant(407, Colors.Blue),
            Timing.Instant(586, Colors.Sun),
            Timing.Instant(705, Colors.Tan),
            Timing.Instant(824, Colors.Green),
            Timing.Instant(943, Colors.Black),
            Timing.Instant(1062, Colors.Red),
            Timing.Instant(1479 , Colors.Blue),
            Timing.Instant(1776, Colors.Sun),
            Timing.Instant(2491, Colors.Green),
            Timing.Transition(2907, 4872, Colors.Steel, Colors.White),
        };

        public override void Generate()
        {
            var layer = GetLayer("Background");

            // Get rid of background
            var mapBg = layer.CreateSprite("b.png");
            mapBg.Fade(0, 0);

            // Add some bleed to sides
            var bg = layer.CreateSprite("m.jpg", OsbOrigin.TopLeft, new Vector2(-108, -1));
            bg.ScaleVec(0, new Vector2(0.46f, 0.46f));
            foreach (var timing in timings)
            {
                if (timing.startTime == timing.endTime)
                {
                    bg.Color(timing.startTime, timing.startTime, timing.startColor, timing.startColor);
                }
                else
                {
                    bg.Color(OsbEasing.In, timing.startTime, timing.endTime, timing.startColor, timing.endColor);

                }
            }

            bg.Fade(170000, 170000, 1, 0);
        }
    }
}
