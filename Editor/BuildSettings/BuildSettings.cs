using System;
using UnityEditor;
using UnityEngine;

namespace FPS
{
    [CreateAssetMenu(menuName = " ")]
    public class BuildSettings : ScriptableObject
    {
        [SerializeField] private BuildTarget buildTarget;

        private void OnValidate()
        {
            const string tgtConst = "BUILD_";
            var targetDefine = tgtConst + buildTarget;
            if (DefineHelper.HasDefine(targetDefine))
                return;

            var enumNames = Enum.GetNames(typeof(BuildTarget));
            foreach (string enumName in enumNames)
                DefineHelper.RemoveCustomDefine(tgtConst + enumName);

            DefineHelper.AddCustomDefine(targetDefine);
        }

        private enum BuildTarget
        {
            WINDOWS,
            ANDROID,
            WEB,
            YANDEX
        }

        private class Creator
        {
            [InitializeOnLoadMethod]
            protected static void TryCreate()
            {
#if !CORE_DEV
                var allInstances = Utils.Editor.GetAllInstances<BuildSettings>();
                if (allInstances.Length > 0)
                    return;

                if (!AssetDatabase.IsValidFolder("Assets/FPS"))
                    AssetDatabase.CreateFolder("Assets", "FPS");

                if (!AssetDatabase.IsValidFolder("Assets/FPS/Editor"))
                    AssetDatabase.CreateFolder("Assets/FPS", "Editor");
                try
                {
                    AssetDatabase.CreateAsset(
                        CreateInstance<BuildSettings>(),
                        $"Assets/FPS/Editor/{nameof(BuildSettings)}.asset");
                }
                catch
                {
                    // ignored
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
#endif
            }
        }
    }
}