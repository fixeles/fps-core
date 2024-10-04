using System;
using Cysharp.Threading.Tasks;
using FPS.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FPS.SFX
{
    public static class FluffyAudio
    {
        // private static readonly Dictionary<string, AudioSource> CancellableSfx = new();
        private static readonly SfxDescription Description;
        private static AudioSource _currentMusic;

        static FluffyAudio()
        {
            //todo: remove resources. Serialize
            Description = Resources.Load<SfxDescription>(nameof(SfxDescription));
        }

        public static void PlaySfx(string key)
        {
            if (!Description.TryGetSfxProtocol(key, out var protocol))
                return;

            var audionSource = FluffyPool.Get<AudioSource>();
            // if (protocol.Cancellable)
            // {
            //     if (CancellableSfx.TryGetValue(key, out AudioSource sfx))
            //     {
            //         sfx.Stop();
            //         CancellableSfx.Remove(key);
            //     }
            //     CancellableSfx.Add(key, audionSource);
            // }

            audionSource.Init(protocol);
            ReleaseAudio(audionSource).Forget();
            audionSource.Play();
        }

        private static async UniTaskVoid ReleaseAudio(AudioSource audioSource)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(audioSource.clip.length));
            FluffyPool.Return(audioSource);
        }

        public static void PlayMusic(string key)
        {
            if (!Description.TryGetMusicProtocol(key, out var protocol))
                return;

            _currentMusic = FluffyPool.Get<AudioSource>();
            _currentMusic.Init(protocol);
            _currentMusic.loop = true;
            _currentMusic.Play();
        }

        public static void StopMusic()
        {
            if (_currentMusic == null)
                return;

            _currentMusic.Stop();
            FluffyPool.Return(_currentMusic);
            //todo add fade
        }

        private static void Init(this AudioSource audioSource, SfxProtocol protocol)
        {
            audioSource.clip = protocol.Clip;
            audioSource.volume = protocol.Volume;

            if (protocol.RandomPitchRange > 0)
                audioSource.pitch = 1 + Random.Range(-protocol.RandomPitchRange, protocol.RandomPitchRange);
        }
    }
}