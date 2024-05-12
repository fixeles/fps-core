using UnityEngine;

namespace FPS.SFX
{
    [CreateAssetMenu]
    public class SfxDescription : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<string, SfxProtocol> sfxSettings;
        [SerializeField] private SerializableDictionary<string, SfxProtocol> musicSettings;

        public bool TryGetSfxProtocol(string key, out SfxProtocol protocol)
        {
            if (!sfxSettings.TryGetValue(key, out protocol))
            {
                Debug.LogWarning($"SFX key {key} doesn't exists");
                return false;
            }

            if (protocol.Clip != null)
                return true;

            Debug.LogWarning($"SFX clip {key} is missing");
            return false;
        }

        public bool TryGetMusicProtocol(string key, out SfxProtocol protocol)
        {
            if (!musicSettings.TryGetValue(key, out protocol))
            {
                Debug.LogWarning($"Music key {key} doesn't exists");
                return false;
            }

            if (protocol.Clip != null)
                return true;

            Debug.LogWarning($"Music clip {key} is missing");
            return false;
        }

#if UNITY_EDITOR
        private class Creator : ScriptableObjectCreator<SfxDescription>
        {
            [UnityEditor.InitializeOnLoadMethod]
            private static void Create()
            {
                TryCreate();
            }
        }
#endif
    }
}