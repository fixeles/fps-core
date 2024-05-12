using System;
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
            if (!DefineHelper.HasDefine(targetDefine))
            {
                var enumNames = Enum.GetNames(typeof(BuildTarget));
                foreach (string enumName in enumNames)
                    DefineHelper.RemoveCustomDefine(tgtConst + enumName);

                DefineHelper.AddCustomDefine(targetDefine);
            }
        }

        private enum BuildTarget
        {
            WINDOWS,
            ANDROID,
            WEB,
            YANDEX
        }
    }
}