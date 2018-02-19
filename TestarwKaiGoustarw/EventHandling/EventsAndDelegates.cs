using System;

namespace TestarwKaiGoustarw.EventHandling
{
    public class EventsAndDelegates
    {
        public class MediaStorage
        {
            public delegate int PlayMedia();

            public void ReportResult(PlayMedia playerDelegate)
            {
                if (playerDelegate() == 0)
                {
                    Console.WriteLine("Media played successfully.");
                }
                else
                {
                    Console.WriteLine("Media did not play successfully.");
                }
            }
        }

        public class AudioPlayer
        {
            private int audioPlayerStatus;

            public int PlayAudioFile()
            {
                Console.WriteLine("Simulating playing an audio file here.");
                audioPlayerStatus = 0;
                return audioPlayerStatus;
            }
        }

        public class Tester
        {
            public void Run()
            {
                MediaStorage myMediaStorage = new MediaStorage();

                // instantiate the two media players
                AudioPlayer myAudioPlayer = new AudioPlayer();

                // instantiate the delegates
                MediaStorage.PlayMedia audioPlayerDelegate = new
                    MediaStorage.PlayMedia(myAudioPlayer.PlayAudioFile);

                // call the delegates
                myMediaStorage.ReportResult(audioPlayerDelegate);

            }
        }

    }
}
