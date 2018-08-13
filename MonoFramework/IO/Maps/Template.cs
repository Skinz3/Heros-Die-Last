using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.IO.Maps
{
    public abstract class Template
    {
        public abstract void Serialize(LittleEndianWriter writer);
        public abstract void Deserialize(LittleEndianReader reader);

        public void Load(string path)
        {
            LittleEndianReader reader = new LittleEndianReader(File.ReadAllBytes(path));
            this.Deserialize(reader);
        }
    }
}
