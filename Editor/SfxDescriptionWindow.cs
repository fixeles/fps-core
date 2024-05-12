using FPS.SFX;
using UnityEditor;

namespace FPS
{
    public class SfxDescriptionWindow
    {
        [MenuItem("FPS/Sfx Description")]
        private static void ShowWindow()
        {
            var poolDescription = Utils.Editor.GetAllInstances<SfxDescription>()[0];
            EditorUtility.OpenPropertyEditor(poolDescription);
        }
    }
}