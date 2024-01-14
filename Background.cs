using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Collections.Generic;

// TODO:
// Make 01:24:370 section come in character by character
// Think of karaoke similar style for 01:52:801
// Fix flash timings
// Maybe regenerate yeah yeah

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

            Framing.New(33, Colors.Red),
            Framing.New(44, Colors.Sun),
            Framing.New(55, Colors.Tan),
            Framing.New(75, Colors.Brown),
            Framing.New(85, 144, Colors.Steel, Colors.White),

            Framing.New(144, 510, Colors.Dark, Colors.White),
            Framing.New(521, Colors.Tan),
            Framing.New(534, Colors.Brown),
            Framing.New(544, Colors.Green),

            // Verse 1
            Framing.New(577, Colors.Cyan),
            Framing.New(646, 718, Colors.Cyan, Colors.White),
            Framing.New(771, 818, Colors.White, Colors.Red),
            Framing.New(887, 898, Colors.Red, Colors.White),
            Framing.New(923, 934, Colors.White, Colors.Red),
            Framing.New(992, 1006, Colors.Red, Colors.White),
            Framing.New(1027, Colors.Red),
            Framing.New(1033, Colors.White),
            Framing.New(1035, Colors.Red),
            Framing.New(1039, Colors.White),
            Framing.New(1043, Colors.Red),

            Framing.New(1058, 1276, Colors.Blue, Colors.Cyan),
            Framing.New(1276, Colors.Blue),
            Framing.New(1278, Colors.White),
            Framing.New(1280, 1485, Colors.Cyan, Colors.Steel),
            Framing.New(1485, 1526, Colors.Steel, Colors.White),

            Framing.New(1526, 1916, Colors.White, Colors.Green),
            Framing.New(1916, 1949, Colors.Green, Colors.Red),

            Framing.New(2004, 2047,  Colors.Red, Colors.Blue),
            Framing.New(2047, 2115, Colors.Blue, Colors.Red),
            Framing.New(2115, 2161, Colors.Red, Colors.Blue),
            Framing.New(2161, 2230, Colors.Blue, Colors.Red),
            Framing.New(2230, 2275, Colors.Red, Colors.Blue),
            Framing.New(2275, 2344, Colors.Blue, Colors.Red),
            Framing.New(2344, 2383, Colors.Red, Colors.Blue),

            Framing.New(2397, Colors.Red),
            Framing.New(2401, Colors.Blue),
            Framing.New(2405, Colors.Red),
            Framing.New(2411, Colors.Blue),

            Framing.New(2428, 2647, Colors.White, Colors.Dark),
            Framing.New(2647, Colors.Sun),
            Framing.New(2649, Colors.White),
            Framing.New(2651, 2840, Colors.Dark, Colors.Steel),

            // yeah yeah
            Framing.New(2840, Colors.Sun),
            Framing.New(2854, Colors.Dark),
            Framing.New(2876, Colors.Sun),
            Framing.New(2882, Colors.White),
            Framing.New(2890, Colors.Sun),
            Framing.New(2904, Colors.Red),
            Framing.New(2911, Colors.Dark),
            Framing.New(2930, Colors.Red),
            Framing.New(2941, Colors.Sun),
            Framing.New(2951, Colors.White),
            Framing.New(2962, Colors.Red),
            Framing.New(2966, Colors.Dark),
            Framing.New(2983, Colors.Sun),
            Framing.New(2993, Colors.White),
            Framing.New(3000, Colors.Sun),
            Framing.New(3004, Colors.Dark),
            Framing.New(3013, Colors.White),
            Framing.New(3016, Colors.Sun),
            Framing.New(3022, Colors.White),
            Framing.New(3029, Colors.Dark),
            Framing.New(3033, Colors.Red),
            Framing.New(3040, Colors.Dark),
            Framing.New(3045, Colors.White),
            Framing.New(3051, Colors.Dark),
            Framing.New(3061, Colors.Sun),
            Framing.New(3076, Colors.Red),
            Framing.New(3082, Colors.Sun),
            Framing.New(3086, Colors.Dark),
            Framing.New(3097, Colors.Red),
            Framing.New(3107, Colors.Sun),
            Framing.New(3112, Colors.White),
            Framing.New(3127, Colors.Sun),
            Framing.New(3133, Colors.Dark),
            Framing.New(3147, Colors.White),
            Framing.New(3165, Colors.Red),
            Framing.New(3173, Colors.Dark),
            Framing.New(3177, Colors.Red),
            Framing.New(3190, Colors.Sun),
            Framing.New(3200, Colors.Dark),
            Framing.New(3229, Colors.Sun),
            Framing.New(3241, Colors.Dark),
            Framing.New(3246, Colors.Red),
            Framing.New(3252, Colors.Dark),
            Framing.New(3257, Colors.Red),
            Framing.New(3262, Colors.White),
            Framing.New(3284, Colors.Red),
            Framing.New(3291, Colors.Dark),
            Framing.New(3299, Colors.Red),
            Framing.New(3301, Colors.White),
            Framing.New(3304, Colors.Sun),
            Framing.New(3312, Colors.White),
            Framing.New(3326, Colors.Sun),

            Framing.New(3344, 3567, Colors.Cyan, Colors.Tan),
            Framing.New(3567, Colors.Sun),
            Framing.New(3569, Colors.White),
            Framing.New(3571, 3746, Colors.Tan, Colors.Blue),

            Framing.New(3754, Colors.White),
            Framing.New(3756, Colors.Cyan),
            Framing.New(3765, Colors.Brown),
            Framing.New(3770, Colors.Tan),

            Framing.New(3777, Colors.Steel),
            Framing.New(3780, Colors.Sun),
            Framing.New(3784, Colors.White),
            Framing.New(3788, Colors.Blue),
            Framing.New(3791, Colors.Green),
            Framing.New(3794, Colors.Dark),
            Framing.New(3798, Colors.White),
            Framing.New(3801, Colors.Cyan),

            Framing.New(4018, Colors.Steel),

            Framing.New(4044, Colors.Steel),
            Framing.New(4104, Colors.White),
            Framing.New(4162, Colors.Red),
            Framing.New(4226, Colors.White),

            Framing.New(4267, Colors.Dark),
            Timing.New(144217, Colors.Red),
            Timing.New(144455, Colors.Tan),
            Timing.New(144693, Colors.Brown),
            Timing.New(144931, Colors.Steel),
            Timing.New(145169, Colors.Green),
            Timing.New(145407, Colors.Sun),
            Timing.New(145526, Colors.Cyan),
            Timing.New(145764, Colors.Dark),

            Framing.New(4382, Colors.White),
            Timing.New(147907, Colors.Blue),
            Timing.New(148145, Colors.Cyan),
            Timing.New(148384, Colors.Sun),
            Timing.New(148622, Colors.Steel),
            Timing.New(148860, Colors.Brown),
            Timing.New(149098, Colors.Tan),
            Timing.New(149217, Colors.Red),
            Timing.New(149574, Colors.White),

            Framing.New(4496, Colors.Blue),
            Timing.New(151836, Colors.Brown),
            Timing.New(152074, Colors.Steel),
            Timing.New(152312, Colors.Red),
            Timing.New(152550, Colors.Green),
            Timing.New(152788, Colors.Cyan),
            Timing.New(153026, Colors.White),
            Timing.New(153145, Colors.Dark),
            Timing.New(153384, Colors.Blue),

            Framing.New(4618, Colors.White),
            Timing.New(157312, Colors.Cyan),
            Timing.New(157669, Colors.Sun),
            Timing.New(158026, Colors.Blue),
            Timing.New(158145, Colors.White),
            Timing.New(158503, Colors.Dark),
            Timing.New(158741, Colors.Green),
            Timing.New(158860, Colors.Brown),
            Timing.New(158979, Colors.Tan),
            Timing.New(159336, Colors.Dark),
            Timing.New(159693, Colors.Sun),
            Timing.New(159812, Colors.Brown),
            Timing.New(160169, Colors.Blue),
            Timing.New(160407, Colors.Green),
            Timing.New(160526, Colors.Tan),
            Timing.New(160645, Colors.Cyan),
            Timing.New(161003, Colors.White),
            Timing.New(161360, Colors.Dark),
            Timing.New(161479, Colors.Sun),
            Timing.New(161836, Colors.Green),
            Timing.New(162193, Colors.Blue),
            Framing.New(4876, Colors.White),
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

            // Hide to show bg slides
            bg.Fade(Framing.Convert(3818), Framing.Convert(3818), 1, 0);

            var bg3 = layer.CreateSprite("m.jpg", OsbOrigin.TopLeft, new Vector2(-108, -1));
            bg3.Color(Framing.Convert(3920), Colors.Dark);
            bg3.ScaleVec(OsbEasing.Out, Framing.Convert(4017), Framing.Convert(4030), new Vector2(0.46f, 0.46f), new Vector2(0, 0.46f));

            var bg2 = layer.CreateSprite("m.jpg", OsbOrigin.TopLeft, new Vector2(-108, -1));
            bg2.Color(Framing.Convert(3865), Colors.Green);
            bg2.ScaleVec(OsbEasing.Out, Framing.Convert(3920), Framing.Convert(3932), new Vector2(0.46f, 0.46f), new Vector2(0, 0.46f));

            var bg1 = layer.CreateSprite("m.jpg", OsbOrigin.TopLeft, new Vector2(-108, -1));
            bg1.Color(Framing.Convert(3818), Colors.Cyan);
            bg1.ScaleVec(OsbEasing.Out, Framing.Convert(3863), Framing.Convert(3876), new Vector2(0.46f, 0.46f), new Vector2(0, 0.46f));

            bg.Fade(Framing.Convert(4017), 1);
            bg.Fade(170000, 170000, 1, 0);
        }
    }
}
