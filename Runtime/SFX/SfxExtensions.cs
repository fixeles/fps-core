using UnityEngine;

namespace FPS.SFX
{
	public static class SfxExtensions
	{
		public static void Init(this AudioSource audioSource, SfxProtocol protocol)
		{
			audioSource.clip = protocol.Clip;
			audioSource.volume = protocol.Volume;

			if (protocol.RandomPitchRange > 0)
				audioSource.pitch = 1 + Random.Range(-protocol.RandomPitchRange, protocol.RandomPitchRange);
		}
	}
}