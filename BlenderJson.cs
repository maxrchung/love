using Newtonsoft.Json;
using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// TODO:
// Fix axes and position conversion
// Key frames
// Support multiple objects
// Test decimate
// Do stuff!!!

namespace StorybrewScripts
{
    public class BlenderJson : StoryboardObjectGenerator
    {
        public class EdgeData
        {
            public EdgeData(StoryboardLayer layer, float time, Vector2 start, Vector2 end)
            {
                Sprite = layer.CreateSprite("w.png", OsbOrigin.CentreLeft, start);

                UpdateSprite(time, start, end);
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

                // Kind of janky to assume initial == 0 but if rotation is truly zero, we should probably use (1,0) anyways?
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
                    Sprite.Move(LastTime, time, LastPosition, start);
                    Sprite.ScaleVec(LastTime, time, LastScaleX, 1, scaleX, 1);
                    Sprite.Rotate(LastTime, time, LastRotation, rotation);
                }

                LastTime = time;
                LastPosition = start;
                LastScaleX = scaleX;
                LastRotation = rotation;
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

        // Blender's animation by default runs at 24 frames per second
        private const float FRAMES_PER_SECOND = 24;
        private float ConvertFrame(float frame)
        {
            return frame * 1000;
            return frame * FRAMES_PER_SECOND / 1000;
        }

        public override void Generate()
        {
            var fileContents = File.ReadAllText("projects/love/love.json");

            /** Example data (for now?):
               
                {
                    "1.0": [
                        [
                            1.5326488018035889,
                            0.48310744762420654,
                            0.7764750123023987,
                            0.8997672200202942,
                            0.6785909533500671,
                            0.9388666749000549
                        ]
                    ],
                }

             */
            var data = JsonConvert.DeserializeObject<SortedDictionary<float, List<List<float>>>>(fileContents);

            var edges = new List<EdgeData>(data.First().Value.Count());
            var layer = GetLayer("Main");

            foreach (var keyframe in data)
            {
                var time = ConvertFrame(keyframe.Key);
                var edgeDatas = keyframe.Value;

                for (var i = 0; i < edgeDatas.Count; ++i)
                {
                    var edgeData = edgeDatas[i];

                    var start = ConvertPosition(new Vector2(edgeData[0], edgeData[1]));
                    var end = ConvertPosition(new Vector2(edgeData[3], edgeData[4]));

                    if (keyframe.Key == data.First().Key)
                    {
                        edges.Add(new EdgeData(layer, time, start, end));
                    }
                    else
                    {
                        edges[i].UpdateSprite(time, start, end);
                    }
                }
            }


            System.Console.WriteLine(data);
        }
    }
}
