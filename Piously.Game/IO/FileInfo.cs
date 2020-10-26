using System.IO;
using Piously.Game.Database;

namespace Piously.Game.IO
{
    public class FileInfo : IHasPrimaryKey
    {
        public int ID { get; set; }
        
        public string Hash { get; set; }

        public string StoragePath => Path.Combine(Hash.Remove(1), Hash.Remove(2), Hash);

        public int ReferenceCount { get; set; }
    }
}
