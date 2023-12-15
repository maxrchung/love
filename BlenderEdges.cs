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

            public void UpdateSprite(float time, Vector2 start, Vector2 end, bool isLastFrame)
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

                if (isLastFrame)
                {
                    Sprite.Fade(time, time, 1, 0);
                }
            }

            public OsbSprite Sprite { get; set; }
            private Vector2 LastPosition { get; set; }
            private float LastScaleX { get; set; }
            private float LastRotation { get; set; }
            private float LastTime { get; set; }
        }

        private static Vector2 SCREEN_SIZE = new Vector2(854, 480);
        private const float SCREEN_OFFSET = -107;

        // Blender position starts (0,0) at bottom left and goes from 0 to 1.
        // Storybrew starts at (-107,-107) top left and goes to (747,587). 
        private Vector2 ConvertPosition(Vector2 position)
        {
            var scaled = position * SCREEN_SIZE + new Vector2(SCREEN_OFFSET);
            var converted = new Vector2(scaled.X, SCREEN_SIZE.Y + SCREEN_OFFSET - scaled.Y);
            return converted;
        }

        private float ConvertFrame(float frame, float framesPerSecond)
        {
            var toMilliseconds = (frame / framesPerSecond) * 1000;
            return toMilliseconds;
        }

        public override void Generate()
        {
            var fileContents = File.ReadAllText("projects/love/love.json");
            var data = JsonConvert.DeserializeObject<BlenderData>(fileContents);
            var framesPerSecond = data.frames_per_second;
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

                        edges[j].UpdateSprite(time, start, end, isLastFrame);
                    }
                }
            }
        }
    }
}
