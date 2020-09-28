namespace Xamarians.MediaPlayer
{
    internal interface INativePlayer
    {
        int Duration { get; }
        int CurrentPosition { get; }
        void Play();
        void Pause();
        void Stop();
        void Seek(int seconds);
    }
}
