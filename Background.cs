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
            Timing.Instant(0, Colors.Night),
            Timing.Instant(169, Colors.Red),
            Timing.Instant(407, Colors.Blue),
            Timing.Instant(586, Colors.Orange),
            Timing.Instant(705, Colors.Brown),
            Timing.Instant(824, Colors.Green),
            Timing.Instant(943, Colors.Black),
            Timing.Instant(1062, Colors.Red),
            Timing.Instant(1479 , Colors.Blue),
            Timing.Instant(1776, Colors.Orange),
            Timing.Instant(2491, Colors.Green),
            Timing.Transition(2907, 4872, Colors.Night, Colors.White),
        };

        public override void Generate()
        {
            var layer = GetLayer("Background");

            // Add some bleed to sides
            var bg = layer.CreateSprite("m.jpg", OsbOrigin.TopLeft, new Vector2(-108, -1));
            bg.ScaleVec(0, new Vector2(0.45f, 0.45f));
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
