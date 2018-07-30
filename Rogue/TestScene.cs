using Microsoft.Xna.Framework;
using MonoFramework.IO;
using MonoFramework.IO.Maps;
using MonoFramework.Objects;
using MonoFramework.Scenes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    public class TestScene : Scene
    {
        GMap Map
        {
            get;
            set;
        }

        public TestScene()
        {
        }

        public override void Dispose()
        {

        }
        MapTemplate template;
        public override void OnInitialize()
        {
            template = new MapTemplate();
            template.Deserialize(new LittleEndianReader(File.ReadAllBytes(Environment.CurrentDirectory + "/test.map")));
            Map = new GMap(new Point(template.Width, template.Height));
            AddObject(Map, LayerEnum.FIRST);
        }

        public override void OnInitializeComplete()
        {
            Map.ToogleDrawRectangles(false);
            Map.Load(template);

        }

        public override void OnUpdate(GameTime time)
        {
        }
    }
}
