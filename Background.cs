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
            Framing.New(0, Colors.White),
            Framing.New(7, Colors.Red),
            Framing.New(14, Colors.Cyan),
            Framing.New(21, Colors.Steel),
            Framing.New(24, Colors.Blue),
            Framing.New(27, Colors.Green),
            Framing.New(30, Colors.Dark),

            Framing.New(35, Colors.Red),
            Framing.New(45, Colors.Sun),
            Framing.New(55, Colors.Tan),
            Framing.New(75, Colors.Brown),
            Framing.New(85, 145, Colors.Steel, Colors.White),

            Framing.New(145, 510, Colors.Dark, Colors.White),
            Framing.New(521, Colors.Tan),
            Framing.New(534, Colors.Brown),
            Framing.New(544, Colors.Green),

            // Verse 1
            Framing.New(577, Colors.Cyan),
            Framing.New(650, 720, Colors.Cyan, Colors.White),
            Framing.New(772, 820, Colors.White, Colors.Red),
            Framing.New(890, 898, Colors.Red, Colors.White),
            Framing.New(924, 938, Colors.White, Colors.Red),
            Framing.New(992, 1007, Colors.Red, Colors.White),
            Framing.New(1028, Colors.Red),
            Framing.New(1033, Colors.White),
            Framing.New(1035, Colors.Red),
            Framing.New(1039, Colors.White),
            Framing.New(1046, Colors.Red),

            Framing.New(1060, 1276, Colors.Blue, Colors.Cyan),
            Framing.New(1276, Colors.White),
            Framing.New(1280, 1485, Colors.Cyan, Colors.Steel),
            Framing.New(1485, 1526, Colors.Steel, Colors.White),

            Framing.New(1526, 1916, Colors.White, Colors.Green),
            Framing.New(1916, 1949, Colors.Green, Colors.Red),

            Framing.New(2004, 2048,  Colors.Red, Colors.Blue),
            Framing.New(2048, 2115, Colors.Blue, Colors.Red),
            Framing.New(2115, 2162, Colors.Red, Colors.Blue),
            Framing.New(2162, 2230, Colors.Blue, Colors.Red),
            Framing.New(2230, 2275, Colors.Red, Colors.Blue),
            Framing.New(2275, 2344, Colors.Blue, Colors.Red),
            Framing.New(2344, 2383, Colors.Red, Colors.Blue),

            Framing.New(2397, Colors.Red),
            Framing.New(2401, Colors.Blue),
            Framing.New(2406, Colors.Red),
            Framing.New(2411, Colors.Blue),

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
