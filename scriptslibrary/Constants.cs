using OpenTK;

namespace StorybrewScripts
{
    public static class Constants
    {
        // This is probably small enough... right?
        public const float MARGIN_OF_ERROR = 0.01f;

        public static Vector2 SCREEN_SIZE = new Vector2(854, 480);
        public static Vector2 SCREEN_OFFSET = new Vector2(-107, 0);

        // Frame rate set in Blender, we do pass this in love.json but it's more
        // practical to just save this as a const
        public static float FRAME_RATE = 30.0f;
    }
}
