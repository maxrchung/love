using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class Background : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("Background");

            // Add some bleed to sides
            var bg = layer.CreateSprite("w.png", OsbOrigin.TopLeft, new Vector2(-108, -1));
            bg.ScaleVec(0, new Vector2(856, 482));
            bg.Color(0, 0.2, 0.2, 0.2);
            bg.Fade(170000, 170000, 1, 0);
        }
    }
}
