using StorybrewCommon.Storyboarding.CommandValues;

namespace StorybrewScripts
{
    public class Timing
    {
        public float startTime { get; set; }
        public float endTime { get; set; }
        public CommandColor startColor { get; set; }
        public CommandColor endColor { get; set; }

        public static Timing Instant(float time, CommandColor color)
        {
            var timing = new Timing()
            {
                startTime = time,
                endTime = time,
                startColor = color,
                endColor = color,
            };
            return timing;
        }

        public static Timing Transition(float startTime, float endTime, CommandColor startColor, CommandColor endColor)
        {
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
