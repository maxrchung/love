using StorybrewCommon.Storyboarding.CommandValues;

namespace StorybrewScripts
{
    public static class Colors
    {
        private static CommandColor Hsb(double hue, double saturation, double brightness)
        {
            return CommandColor.FromHsb(hue, saturation, brightness);
        }

        public static CommandColor White = Hsb(252, 0.2, 0.99);
        public static CommandColor Red = Hsb(7.43, 0.98, 0.83);
        public static CommandColor Blue = Hsb(205, 0.8, 0.67);
        public static CommandColor Orange = Hsb(37, 0.54, 0.96);
        public static CommandColor Brown = Hsb(46, 0.18, 0.66);
        public static CommandColor Green = Hsb(86, 0.26, 0.26);
        public static CommandColor Grey = Hsb(235, 0.3, 0.5);
        public static CommandColor Night = Hsb(235, 0.45, 0.35);
        public static CommandColor Black = Hsb(32, 0.47, 0.23);
    }
}
