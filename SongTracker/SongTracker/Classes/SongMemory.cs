using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace SongTracker.Classes
{
    class SongMemory
    {
        public List<Song> SaveSongs(List<Song> songList)
        {
            var sr = new Serializer();
            sr.JsonSerializer(songList, ConfigurationManager.AppSettings["FilePath"]);
            return songList;
        }
        public List<Song> AddSong(List<Song> songList, Song song)
        {
            var listanBiisi = songList.FirstOrDefault(x => x.Name.ToLower() == song.Name.ToLower());
            if (listanBiisi == null)
            {
                songList.Add(song);
            }
            else
            {
                Console.WriteLine($"Could not add { song.Name }, same song name alrady exists!");
            }
            return songList;
        }
        public List<Song> DeleteSong(List<Song> songList, Song song)
        {
            var poistettavaSong = songList.FirstOrDefault(x => x.Name.ToLower() == song.Name.ToLower());
            if (poistettavaSong != null)
            {
                songList.Remove(song);
            }
            return songList;
        }
        public List<Song> EditSong(List<Song> songList, Song song)
        {
            var listanBiisi = songList.FirstOrDefault(x => x.Name.ToLower() == song.Name.ToLower());
            if (listanBiisi != null)
            {
                songList.Remove(listanBiisi);
                songList.Add(song);
            }
            return songList;
        }
        public List<Song> GetSongs()
        {
            var ds = new Serializer();
            var gotList = ds.JsonDeserializer(ConfigurationManager.AppSettings["FilePath"]);
            return gotList;
        }
        public Boolean songExists(List<Song> songList, Song song)
        {
            var listanBiisi = songList.FirstOrDefault(x => x.Name.ToLower() == song.Name.ToLower());
            if (listanBiisi != null)
            {
                return true;
            }
            return false;
        }
    }
}
