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

        public static CommandColor Cyan = Hsb(205, 0.82, 0.73);
        public static CommandColor Steel = Hsb(235, 0.3, 0.5);
        public static CommandColor Blue = Hsb(234, 0.47, 0.34);
        public static CommandColor Green = Hsb(86, 0.26, 0.26);

        public static CommandColor Red = Hsb(7.43, 0.98, 0.83);
        public static CommandColor Sun = Hsb(37, 0.54, 0.96);
        public static CommandColor Tan = Hsb(46, 0.18, 0.66);
        public static CommandColor Brown = Hsb(31, 0.5, 0.31);

        public static CommandColor Black = Hsb(33, 0.56, 0.18);

        public static string GetSprite(CommandColor color)
        {
            if (color == White)
                return "0.png";
            else if (color == Cyan)
                return "1.png";
            else if (color == Steel)
                return "2.png";
            else if (color == Blue)
                return "3.png";
            else if (color == Green)
                return "4.png";
            else if (color == Red)
                return "5.png";
            else if (color == Sun)
                return "6.png";
            else if (color == Tan)
                return "7.png";
            else if (color == Brown)
                return "8.png";
            else if (color == Black)
                return "9.png";

            return "-1";
        }
    }
}
