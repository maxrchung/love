using Newtonsoft.Json;
using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StorybrewScripts
{
    using ObjectData = SortedDictionary<float, List<List<float>>>;

    public class BlenderEdges : StoryboardObjectGenerator
    {
        // This is probably small enough... right?
        const float MARGIN_OF_ERROR = 0.01f;

        /** Example data (for now?):
            {
                frames_per_second: 30,
                objects: [{
                    "1.0": [[
                        1.5326488018035889,
                        0.48310744762420654,
                        0.7764750123023987,
                        0.8997672200202942,
                        0.6785909533500671,
                        0.9388666749000549
                    ]]
                }],
            }
         */
        public class BlenderData
        {
            public float frames_per_second { get; set; }
            public List<ObjectData> objects { get; set; }
        }

        public class Edge
        {
            public Edge(StoryboardLayer layer, Vector2 start)
            {
                Sprite = layer.CreateSprite("w.png", OsbOrigin.CentreLeft, start);
            }

            // Just copied some stuff from here??? https://stackoverflow.com/a/16544330
            private float AngleBetween(Vector2 start, Vector2 end)
            {
                return (float)(Math.Atan2(
                    start.X * end.Y - start.Y * end.X,
                    start.X * end.X + start.Y * end.Y
                ));
            }

            public void UpdateSprite(float time, Vector2 start, Vector2 end)
            {
                var diff = end - start;
                var scaleX = diff.Length;

                // Kind of janky to assume initial == 0 but if rotation is zero, we should probably use (1,0) anyways?
                var last = LastRotation == 0
                    ? new Vector2(1, 0)
                    : new Vector2((float)Math.Cos(LastRotation), (float)Math.Sin(LastRotation));

                var rotation = LastRotation + AngleBetween(last, diff);

                if (Sprite.CommandCount == 0) // Initial commands
                {
                    Sprite.ScaleVec(time, scaleX, 1);
                    Sprite.Rotate(time, rotation);
                }
                else
                {
                    // Save on some operations by skipping them if less than some amount
                    if (Math.Abs((start - LastPosition).Length) > MARGIN_OF_ERROR)
                    {
                        Sprite.Move(LastTime, time, LastPosition, start);
                    }

                    if (Math.Abs(scaleX - LastScaleX) > MARGIN_OF_ERROR)
                    {
                        Sprite.ScaleVec(LastTime, time, LastScaleX, 1, scaleX, 1);
                    }

                    // Hopefully for rotation this margin of error is small enough
                    if (Math.Abs(rotation - LastRotation) > MARGIN_OF_ERROR)
                    {
                        Sprite.Rotate(LastTime, time, LastRotation, rotation);
                    }
                }

                LastTime = time;
                LastPosition = start;
                LastScaleX = scaleX;
                LastRotation = rotation;
            }

            public void Disappear(float time)
            {
                Sprite.Fade(time, time, 1, 0);
            }

            public OsbSprite Sprite { get; set; }
            private Vector2 LastPosition { get; set; }
            private float LastScaleX { get; set; }
            private float LastRotation { get; set; }
            private float LastTime { get; set; }
        }

        private static Vector2 SCREEN_SIZE = new Vector2(854, 480);
        private static Vector2 SCREEN_OFFSET = new Vector2(-107, 0);

        // Blender position starts (0,0) at bottom left and goes from 0 to 1.
        // Storybrew starts at (-107,0) top left and goes to (747,587). wtf?
        private Vector2 ConvertPosition(Vector2 position)
        {
            var scaled = position * SCREEN_SIZE + SCREEN_OFFSET;
            var converted = new Vector2(scaled.X, SCREEN_SIZE.Y - scaled.Y);
            return converted;
        }

        private float ConvertFrame(float frame, float framesPerSecond)
        {
            var toMilliseconds = (frame / framesPerSecond) * 1000;
            return toMilliseconds;
        }

        private void GenerateEnd(string fileSuffix, float startTime, List<float> disappearTimes)
        {
            // Hvae to GetLayer at top level
            var layer = GetLayer("Main");
            var edges = new List<Edge>();

            var fileContents = File.ReadAllText($"projects/love/endstuff{fileSuffix}.001.json");
            var data = JsonConvert.DeserializeObject<List<List<float>>>(fileContents);

            foreach (var edgeData in data)
            {
                var start = ConvertPosition(new Vector2(edgeData[0], edgeData[1]));
                var end = ConvertPosition(new Vector2(edgeData[2], edgeData[3]));

                var edge = new Edge(layer, start);
                edges.Add(edge);
                edge.UpdateSprite(startTime, start, end);
            }

            foreach (var edge in edges)
            {
                var disappearTime = disappearTimes[Random(disappearTimes.Count)];
                edge.Disappear(disappearTime);
            }
        }

        public override void Generate()
        {
            var fileContents = File.ReadAllText("projects/love/love.json");
            var data = JsonConvert.DeserializeObject<BlenderData>(fileContents);
            var framesPerSecond = data.frames_per_second;

            var disappear1 = new List<float>() { 144217, 144455, 144693, 144931, 145169, 145407, 145526 };
            GenerateEnd("11", ConvertFrame(4268, framesPerSecond), disappear1);
            GenerateEnd("12", ConvertFrame(4272, framesPerSecond), disappear1);
            GenerateEnd("13", ConvertFrame(4276, framesPerSecond), disappear1);
            GenerateEnd("14", ConvertFrame(4283, framesPerSecond), disappear1);
            GenerateEnd("15", ConvertFrame(4291, framesPerSecond), disappear1);
            GenerateEnd("16", ConvertFrame(4302, framesPerSecond), disappear1);

            var disappear2 = new List<float>() { 147907, 148145, 148384, 148622, 148860, 149098, 149217 };
            GenerateEnd("21", ConvertFrame(4382, framesPerSecond), disappear2);
            GenerateEnd("22", ConvertFrame(4385, framesPerSecond), disappear2);
            GenerateEnd("23", ConvertFrame(4390, framesPerSecond), disappear2);
            GenerateEnd("24", ConvertFrame(4395, framesPerSecond), disappear2);
            GenerateEnd("25", ConvertFrame(4404, framesPerSecond), disappear2);
            GenerateEnd("26", ConvertFrame(4416, framesPerSecond), disappear2);

            var disappear3 = new List<float>() { 151836, 152074, 152312, 152550, 152788, 153026, 153145 };
            GenerateEnd("31", ConvertFrame(4497, framesPerSecond), disappear3);
            GenerateEnd("32", ConvertFrame(4500, framesPerSecond), disappear3);
            GenerateEnd("33", ConvertFrame(4505, framesPerSecond), disappear3);
            GenerateEnd("34", ConvertFrame(4511, framesPerSecond), disappear3);
            GenerateEnd("35", ConvertFrame(4519, framesPerSecond), disappear3);
            GenerateEnd("36", ConvertFrame(4531, framesPerSecond), disappear3);

            var disappear4 = new List<float>() { 157312, 157669, 158026, 158145, 158503, 158622, 158860, 158741, 158503, 158741, 159336, 159693, 159812, 160169, 160288, 160407, 160526, 160645, 160764, 161003, 161360, 161479, 161836, 162193 };
            GenerateEnd("41", ConvertFrame(4618, framesPerSecond), disappear4);
            GenerateEnd("42", ConvertFrame(4633, framesPerSecond), disappear4);
            GenerateEnd("43", ConvertFrame(4647, framesPerSecond), disappear4);
            GenerateEnd("44", ConvertFrame(4660, framesPerSecond), disappear4);
            GenerateEnd("45", ConvertFrame(4670, framesPerSecond), disappear4);
            GenerateEnd("46", ConvertFrame(4681, framesPerSecond), disappear4);
            GenerateEnd("47", ConvertFrame(4690, framesPerSecond), disappear4);

            var objects = data.objects;

            // Hvae to GetLayer at top level
            var layer = GetLayer("Main");

            foreach (var obj in objects)
            {
                var edges = new List<Edge>(obj.First().Value.Count());

                foreach (var keyframe in obj)
                {
                    var isLastFrame = keyframe.Key == obj.Last().Key;
                    var time = ConvertFrame(keyframe.Key, framesPerSecond);
                    var edgeDatas = keyframe.Value;

                    for (var j = 0; j < edgeDatas.Count; ++j)
                    {
                        var edgeData = edgeDatas[j];
                        var start = ConvertPosition(new Vector2(edgeData[0], edgeData[1]));
                        var end = ConvertPosition(new Vector2(edgeData[2], edgeData[3]));

                        if (keyframe.Key == obj.First().Key)
                        {
                            edges.Add(new Edge(layer, start));
                        }

                        edges[j].UpdateSprite(time, start, end);

                        if (isLastFrame)
                        {
                            edges[j].Disappear(time);
                        }
                    }
                }
            }
        }
    }
}
