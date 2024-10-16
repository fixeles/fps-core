using UnityEditor;

namespace FPS
{
    public static class BuildSettingsWindow
    {
        [MenuItem("FPS/Build Settings")]
        private static void ShowWindow()
        {
            var settings = Utils.Editor.GetAllInstances<BuildSettings>()[0];
            EditorUtility.OpenPropertyEditor(settings);
        }
    }
}