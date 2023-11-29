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
            var bg = layer.CreateSprite("w.png", OsbOrigin.TopLeft, new Vector2(800, 200));
            bg.Scale(5, 5);
            bg.Fade(0, 2000, 0, 1);
            bg.Fade(8000, 10000, 1, 0);
        }
    }
}
