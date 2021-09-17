using Ardalis.GuardClauses;
using Plugin.SimpleAudioPlayer;
using System.IO;

namespace ChatClient.Services
{
    public class AudioPlayer
    {
        private readonly ISimpleAudioPlayer _player;

        public AudioPlayer(IFileSystem fileSystem)
        {
            Guard.Against.Null(fileSystem, nameof(fileSystem));

            var audioPath = Path.Combine(fileSystem.AudioDir, "new_message.wav");
            _player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            _player.Load("new_message.wav");
        }

        public void PlayNotificationSound()
        {
            _player.Play();
        }
    }
}
