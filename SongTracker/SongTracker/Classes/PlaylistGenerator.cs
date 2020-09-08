using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SongTracker.Classes
{
    class PlaylistGenerator
    {
        public List<Song> SongList { get; set; }
        public PlaylistGenerator(List<Song> Songlist)
        {
            SongList = Songlist;
        }
        public List<Song> GeneratePlaylist(int RequestedListLength)
        {
            // tästä tehdään soittolista
            var Playlist = new List<Song>();
            //soittolista voi olla vain niin pitkä kuin olemassaolevien kappaleiden lkm
            if (SongList.Count < RequestedListLength)
            {
                RequestedListLength = SongList.Count;
            }
            //tehdään kopio SongLististä apumuuttujaksi
            var CopySongList = new ArrayList();
            foreach (Song song in SongList)
            {
                CopySongList.Add(song);
            }
            //aloitetaan apumuuttujalistan ekasta indeksistä prioriteettivertailu apumuuttujalla
            for (int i = 0; i < RequestedListLength; i++)
            {
                int GreatestPriority = ((Song)CopySongList[0]).GetPriority();
                //vertaillaan jokaiseen CopySongListin kappaleen prioriteettiin etsien suurin
                foreach (Song song in CopySongList)
                {
                    if (song.GetPriority() > GreatestPriority)
                    {
                        GreatestPriority = song.GetPriority();
                    }
                }
                //poistetaan CopySongListilta suurimman prioriteetin kappale ja lisätään playlistille
                foreach (Song song in CopySongList)
                {
                    if (song.GetPriority() == GreatestPriority)
                    {
                        Playlist.Add(song);
                        CopySongList.Remove(song);
                        break;
                    }
                }
            }
            return Playlist;
        }
    }
}
