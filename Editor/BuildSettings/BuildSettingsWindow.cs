using UnityEditor;

namespace FPS
{
    public class BuildSettingsWindow
    {
        [MenuItem("FPS/Build Settings")]
        private static void ShowWindow()
        {
            var poolDescription = Utils.Editor.GetAllInstances<BuildSettings>()[0];
            EditorUtility.OpenPropertyEditor(poolDescription);
        }
    }
}