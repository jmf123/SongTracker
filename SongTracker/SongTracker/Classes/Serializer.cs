using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SongTracker.Classes
{
    class Serializer
    {
        public void JsonSerializer(List<Song> data, string filePath)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, serializedData);
        }
        public List<Song> JsonDeserializer(string filePath)
        {
            return JsonConvert.DeserializeObject<List<Song>>(System.IO.File.ReadAllText(filePath));
        }
    }
}
