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
            bg.Color(0, 0.2, 0.2, 0.2);
            bg.Fade(999999, 999999, 1, 0);
        }
    }
}
