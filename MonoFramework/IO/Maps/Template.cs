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

        public string Path
        {
            get;
            set;
        }
        public void Load(string path)
        {
            LittleEndianReader reader = new LittleEndianReader(File.ReadAllBytes(path));
            this.Deserialize(reader);
            this.Path = path;
        }
        public void Save()
        {
            LittleEndianWriter writer = new LittleEndianWriter();
            Serialize(writer);
            File.WriteAllBytes(Path, writer.Data);
        }
    }
}
