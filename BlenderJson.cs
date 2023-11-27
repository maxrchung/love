using Newtonsoft.Json;
using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StorybrewScripts
{

    public class BlenderJson : StoryboardObjectGenerator
    {
        public class EdgeData
        {
            public EdgeData(StoryboardLayer layer, float time, Vector2 start, Vector2 end)
            {
                Sprite = layer.CreateSprite("w.png", OsbOrigin.CentreLeft, start);

                var distance = end - start;
                var scale = new Vector2(distance.Length, 1);
                Sprite.ScaleVec(time, scale);

                var rotation = Vector3.CalculateAngle(new Vector3(start), new Vector3(end));
                Sprite.Rotate(time, rotation);

                LastTime = time;
                LastPosition = start;
                LastScale = scale;
                LastRotation = rotation;
            }

            public void Transition(float time, Vector2 start, Vector2 end)
            {
                Sprite.Move(LastTime, time, LastPosition, start);

                var distance = end - start;
                var scale = new Vector2(distance.Length, 1);
                Sprite.ScaleVec(LastTime, time, LastScale, scale);

                var rotation = Vector3.CalculateAngle(new Vector3(start), new Vector3(end));
                Sprite.Rotate(LastTime, time, LastRotation, rotation);

                LastTime = time;
                LastPosition = start;
                LastScale = scale;
                LastRotation = rotation;

            }

            public OsbSprite Sprite { get; }

            private Vector2 LastPosition { get; set; }
            private Vector2 LastScale { get; set; }
            private float LastRotation { get; set; }
            private float LastTime { get; set; }
        }

        public override void Generate()
        {
            var fileContents = File.ReadAllText("projects/love/love.json");
            var data = JsonConvert.DeserializeObject<SortedDictionary<float, List<List<float>>>>(fileContents);

            var edges = new List<EdgeData>(data.First().Value.Count());
            var layer = GetLayer("Main");

            foreach (var keyframe in data)
            {
                var time = keyframe.Key;
                var edgeDatas = keyframe.Value;

                for (var i = 0; i < edgeDatas.Count; ++i)
                {
                    var edgeData = edgeDatas[i];

                    var start = new Vector2(edgeData[0], edgeData[1]);
                    var end = new Vector2(edgeData[3], edgeData[4]);

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
