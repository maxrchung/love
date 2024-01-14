using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Collections.Generic;

// TODO:
// Make 01:24:370 section come in character by character
// Think of karaoke similar style for 01:52:801

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
            Framing.New(1276, Colors.Blue),
            Framing.New(1278, Colors.White),
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

            Framing.New(2429, 2649, Colors.White, Colors.Dark),
            Framing.New(2649, Colors.Sun),
            Framing.New(2651, Colors.White),
            Framing.New(2653, 2840, Colors.Dark, Colors.Steel),

            // yeah yeah
            Framing.New(2841, Colors.Sun),
            Framing.New(2855, Colors.Dark),
            Framing.New(2877, Colors.Sun),
            Framing.New(2883, Colors.White),
            Framing.New(2890, Colors.Sun),
            Framing.New(2905, Colors.Red),
            Framing.New(2913, Colors.Dark),
            Framing.New(2930, Colors.Red),
            Framing.New(2941, Colors.Sun),
            Framing.New(2951, Colors.White),
            Framing.New(2962, Colors.Red),
            Framing.New(2966, Colors.Dark),
            Framing.New(2984, Colors.Sun),
            Framing.New(2993, Colors.White),
            Framing.New(3001, Colors.Sun),
            Framing.New(3004, Colors.Dark),
            Framing.New(3013, Colors.White),
            Framing.New(3017, Colors.Sun),
            Framing.New(3022, Colors.White),
            Framing.New(3030, Colors.Dark),
            Framing.New(3033, Colors.Red),
            Framing.New(3041, Colors.Dark),
            Framing.New(3046, Colors.White),
            Framing.New(3051, Colors.Dark),
            Framing.New(3061, Colors.Sun),
            Framing.New(3076, Colors.Red),
            Framing.New(3082, Colors.Sun),
            Framing.New(3087, Colors.Dark),
            Framing.New(3097, Colors.Red),
            Framing.New(3108, Colors.Sun),
            Framing.New(3118, Colors.White),
            Framing.New(3128, Colors.Sun),
            Framing.New(3134, Colors.Dark),
            Framing.New(3148, Colors.White),
            Framing.New(3166, Colors.Red),
            Framing.New(3173, Colors.Dark),
            Framing.New(3178, Colors.Red),
            Framing.New(3191, Colors.Sun),
            Framing.New(3199, Colors.Dark),
            Framing.New(3230, Colors.Sun),
            Framing.New(3241, Colors.Dark),
            Framing.New(3246, Colors.Red),
            Framing.New(3252, Colors.Dark),
            Framing.New(3257, Colors.Red),
            Framing.New(3262, Colors.White),
            Framing.New(3285, Colors.Red),
            Framing.New(3291, Colors.Dark),
            Framing.New(3299, Colors.Red),
            Framing.New(3302, Colors.White),
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

            Framing.New(4045, Colors.Steel),
            Framing.New(4105, Colors.White),
            Framing.New(4163, Colors.Red),
            Framing.New(4227, Colors.White),

            Framing.New(4268, Colors.Dark),
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

            Framing.New(4497, Colors.Blue),
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
            Timing.New(158622, Colors.Tan),
            Timing.New(158860, Colors.Brown),
            Timing.New(158741, Colors.Green),
            Timing.New(158503, Colors.Cyan),
            Timing.New(158741, Colors.White),
            Timing.New(159336, Colors.Dark),
            Timing.New(159693, Colors.Sun),
            Timing.New(159812, Colors.Brown),
            Timing.New(160169, Colors.Blue),
            Timing.New(160288, Colors.Steel),
            Timing.New(160407, Colors.Green),
            Timing.New(160526, Colors.Tan),
            Timing.New(160645, Colors.Cyan),
            Timing.New(160764, Colors.White),
            Timing.New(161003, Colors.Cyan),
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
            bg.Fade(Framing.Convert(3819), Framing.Convert(3819), 1, 0);

            var bg3 = layer.CreateSprite("m.jpg", OsbOrigin.TopLeft, new Vector2(-108, -1));
            bg3.ScaleVec(Framing.Convert(3920), new Vector2(0.46f, 0.46f));
            bg3.Color(Framing.Convert(3920), Colors.Dark);
            bg3.MoveX(OsbEasing.Out, Framing.Convert(4018), Framing.Convert(4030), -108, -108 - 855);

            var bg2 = layer.CreateSprite("m.jpg", OsbOrigin.TopLeft, new Vector2(-108, -1));
            bg2.ScaleVec(Framing.Convert(3865), new Vector2(0.46f, 0.46f));
            bg2.Color(Framing.Convert(3865), Colors.Green);
            bg2.MoveX(OsbEasing.Out, Framing.Convert(3920), Framing.Convert(3932), -108, -108 - 855);

            var bg1 = layer.CreateSprite("m.jpg", OsbOrigin.TopLeft, new Vector2(-108, -1));
            bg1.ScaleVec(Framing.Convert(3819), new Vector2(0.46f, 0.46f));
            bg1.Color(Framing.Convert(3819), Colors.Cyan);
            bg1.MoveX(OsbEasing.Out, Framing.Convert(3865), Framing.Convert(3877), -108, -108 - 855);

            bg.Fade(Framing.Convert(4018), 1);
            bg.Fade(170000, 170000, 1, 0);
        }
    }
}
