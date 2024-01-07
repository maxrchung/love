using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class Bars : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("Bars");

            // Add some bleed to sides
            var top = layer.CreateSprite("0.png", OsbOrigin.TopLeft, new Vector2(-108, -1));
            top.ScaleVec(OsbEasing.In, 123979, 124812, 856, 0, 856, 66);

            var bottom = layer.CreateSprite("0.png", OsbOrigin.BottomLeft, new Vector2(-108, 480 + 1));
            bottom.ScaleVec(OsbEasing.In, 123979, 124812, 856, 0, 856, 66);

            top.ScaleVec(OsbEasing.In, 157431, 168264, 856, 66, 856, 244);
            bottom.ScaleVec(OsbEasing.In, 157431, 168264, 856, 66, 856, 244);

            top.Color(OsbEasing.In, 162431, 168264, Colors.Black, Colors.Cyan);
            bottom.Color(OsbEasing.In, 162431, 168264, Colors.Black, Colors.Cyan);

            top.Fade(170000, 170000, 1, 0);
            bottom.Fade(170000, 170000, 1, 0);
        }
    }
}
