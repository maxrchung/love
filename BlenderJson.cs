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

                var test = layer.CreateSprite("w.png", OsbOrigin.CentreLeft, start);
                test.Scale(1000, 10000, 5, 5);

                UpdateSprite(time, start, end);
            }

            public void Transition(float time, Vector2 start, Vector2 end)
            {
                UpdateSprite(time, start, end);
            }

            // This helped: https://straypixels.net/angle-between-vectors/
            // https://stackoverflow.com/questions/14066933/direct-way-of-computing-the-clockwise-angle-between-two-vectors
            private float AngleBetween(Vector2 start, Vector2 end)
            {
                return (float)(Math.Atan2(
                    -Vector3.Cross(new Vector3(start), new Vector3(end)).Length,
                    -Vector2.Dot(start, end)
                ) + Math.PI);
            }

            private void UpdateSprite(float time, Vector2 start, Vector2 end)
            {
                var scaleX = (end - start).Length;
                var rotation = Vector3.CalculateAngle(new Vector3(start), new Vector3(end));
                //var rotation = AngleBetween(start, end);

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

            /** Example data (for now):
               
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
                        edges[i].Transition(time, start, end);
                    }
                }
            }


            System.Console.WriteLine(data);
        }
    }
}
