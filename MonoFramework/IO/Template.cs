using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.IO
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
        public bool Load(string path)
        {
            if (File.Exists(path) == false)
            {
                return false;
            }

            LittleEndianReader reader = new LittleEndianReader(File.ReadAllBytes(path));
            try
            {
                this.Deserialize(reader);
            }
            catch
            {
                return false;
            }
            this.Path = path;
            return true;
        }
        public void Save()
        {
            LittleEndianWriter writer = new LittleEndianWriter();
            Serialize(writer);
            File.WriteAllBytes(Path, writer.Data);
        }
    }
}
