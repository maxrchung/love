using StorybrewCommon.Storyboarding.CommandValues;

namespace StorybrewScripts
{
    public class Framing
    {
        public float startTime { get; set; }
        public float endTime { get; set; }
        public CommandColor startColor { get; set; }
        public CommandColor endColor { get; set; }

        public static float Convert(float frame)
        {
            var toMilliseconds = (frame / Constants.FRAME_RATE) * 1000;
            return toMilliseconds;
        }

        public static Timing New(float frame, CommandColor color)
        {
            var time = Convert(frame);

            var timing = new Timing()
            {
                startTime = time,
                endTime = time,
                startColor = color,
                endColor = color,
            };
            return timing;
        }

        public static Timing New(float startFrame, float endFrame, CommandColor startColor, CommandColor endColor)
        {
            var startTime = Convert(startFrame);
            var endTime = Convert(endFrame);

            var timing = new Timing()
            {
                startTime = startTime,
                endTime = endTime,
                startColor = startColor,
                endColor = endColor,
            };
            return timing;
        }
    }
}
