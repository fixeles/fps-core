using System;
using UnityEngine;

namespace FPS.SFX
{
    [Serializable]
    public struct SfxProtocol
    {
        public AudioClip Clip;
        [field: SerializeField, Range(0f, 1f)] public float Volume { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float RandomPitchRange { get; private set; }
    }
}