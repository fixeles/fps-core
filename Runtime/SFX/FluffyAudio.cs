using System;
using Cysharp.Threading.Tasks;
using FPS.Pool;
using UnityEngine;
using VContainer;

namespace FPS.SFX
{
	public class FluffyAudio : IAudioService
	{
		private readonly IObjectPool _pool;

		// private static readonly Dictionary<string, AudioSource> CancellableSfx = new();
		private readonly SfxDescription _description;
		private AudioSource _currentMusic;

		[Inject]
		public FluffyAudio(IObjectPool pool)
		{
			_pool = pool;
			//todo: remove resources. Serialize
			_description = Resources.Load<SfxDescription>(nameof(SfxDescription));
		}

		public void PlaySfx(string key)
		{
			if (!_description.TryGetSfxProtocol(key, out var protocol))
				return;

			var audionSource = _pool.Get<AudioSource>();
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

		private async UniTaskVoid ReleaseAudio(AudioSource audioSource)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(audioSource.clip.length));
			_pool.Return(audioSource);
		}

		public void PlayMusic(string key)
		{
			if (!_description.TryGetMusicProtocol(key, out var protocol))
				return;

			_currentMusic = _pool.Get<AudioSource>();
			_currentMusic.Init(protocol);
			_currentMusic.loop = true;
			_currentMusic.Play();
		}

		public void StopMusic()
		{
			if (_currentMusic == null)
				return;

			_currentMusic.Stop();
			_pool.Return(_currentMusic);
			//todo add fade
		}
	}
}