using System;

namespace Xamarians.MediaPlayer
{
    public interface IVideoPlayer
    {
        int PlaybackRate { get; set; }
        bool AutoPlay { get; set; }
        int CurrentPosition { get; }
        int Duration { get; }
        string Source { get; set; }

        event EventHandler Prepared;
        event EventHandler<PlayerErrorEventArgs> Error;
        event EventHandler Completed;

        void Play();
        void Pause();
        void Stop();
        void Seek(int seconds);
    }
}
