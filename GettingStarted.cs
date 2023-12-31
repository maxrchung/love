using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class GettingStarted : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("Main");
            var bg = layer.CreateSprite("w.png", OsbOrigin.TopLeft, new Vector2(-107, 0));
            bg.ScaleVec(0, new Vector2(854, 480));
            bg.Color(0, 0.1, 0.1, 0.1);
            bg.Fade(170000, 170000, 1, 0);
        }
    }
}
