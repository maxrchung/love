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
                    if (Math.Abs((start - LastPosition).Length) > Constants.MARGIN_OF_ERROR)
                    {
                        Sprite.Move(LastTime, time, LastPosition, start);
                    }

                    if (Math.Abs(scaleX - LastScaleX) > Constants.MARGIN_OF_ERROR)
                    {
                        Sprite.ScaleVec(LastTime, time, LastScaleX, 1, scaleX, 1);
                    }

                    // Hopefully for rotation this margin of error is small enough
                    if (Math.Abs(rotation - LastRotation) > Constants.MARGIN_OF_ERROR)
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

        // Blender position starts (0,0) at bottom left and goes from 0 to 1.
        // Storybrew starts at (-107,0) top left and goes to (747,587). wtf?
        private Vector2 ConvertPosition(Vector2 position)
        {
            var scaled = position * Constants.SCREEN_SIZE + Constants.SCREEN_OFFSET;
            var converted = new Vector2(scaled.X, Constants.SCREEN_SIZE.Y - scaled.Y);
            return converted;
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

            var disappear1 = new List<float>() { 144217, 144455, 144693, 144931, 145169, 145407, 145526 };
            GenerateEnd("11", Framing.Convert(4268), disappear1);
            GenerateEnd("12", Framing.Convert(4272), disappear1);
            GenerateEnd("13", Framing.Convert(4276), disappear1);
            GenerateEnd("14", Framing.Convert(4283), disappear1);
            GenerateEnd("15", Framing.Convert(4291), disappear1);
            GenerateEnd("16", Framing.Convert(4302), disappear1);

            var disappear2 = new List<float>() { 147907, 148145, 148384, 148622, 148860, 149098, 149217 };
            GenerateEnd("21", Framing.Convert(4382), disappear2);
            GenerateEnd("22", Framing.Convert(4385), disappear2);
            GenerateEnd("23", Framing.Convert(4390), disappear2);
            GenerateEnd("24", Framing.Convert(4395), disappear2);
            GenerateEnd("25", Framing.Convert(4404), disappear2);
            GenerateEnd("26", Framing.Convert(4416), disappear2);

            var disappear3 = new List<float>() { 151836, 152074, 152312, 152550, 152788, 153026, 153145 };
            GenerateEnd("31", Framing.Convert(4497), disappear3);
            GenerateEnd("32", Framing.Convert(4500), disappear3);
            GenerateEnd("33", Framing.Convert(4505), disappear3);
            GenerateEnd("34", Framing.Convert(4511), disappear3);
            GenerateEnd("35", Framing.Convert(4519), disappear3);
            GenerateEnd("36", Framing.Convert(4531), disappear3);

            var disappear4 = new List<float>() { 157312, 157669, 158026, 158145, 158503, 158622, 158860, 158741, 158503, 158741, 159336, 159693, 159812, 160169, 160288, 160407, 160526, 160645, 160764, 161003, 161360, 161479, 161836, 162193 };
            GenerateEnd("41", Framing.Convert(4618), disappear4);
            GenerateEnd("42", Framing.Convert(4633), disappear4);
            GenerateEnd("43", Framing.Convert(4647), disappear4);
            GenerateEnd("44", Framing.Convert(4660), disappear4);
            GenerateEnd("45", Framing.Convert(4670), disappear4);
            GenerateEnd("46", Framing.Convert(4681), disappear4);
            GenerateEnd("47", Framing.Convert(4690), disappear4);

            var objects = data.objects;

            // Hvae to GetLayer at top level
            var layer = GetLayer("Main");

            foreach (var obj in objects)
            {
                var edges = new List<Edge>(obj.First().Value.Count());

                foreach (var keyframe in obj)
                {
                    var isLastFrame = keyframe.Key == obj.Last().Key;
                    var time = Framing.Convert(keyframe.Key);
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
