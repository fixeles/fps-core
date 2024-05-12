using FPS.SFX;
using UnityEditor;

namespace FPS
{
    public class SfxDescriptionWindow
    {
        [MenuItem("FPS/Sfx Description")]
        private static void ShowWindow()
        {
#if CORE_DEV
            var description = Utils.Editor.GetAllInstances<SfxDescription>()[0];
#else
            var description = UnityEngine.Resources.Load<SfxDescription>(nameof(SfxDescription));
#endif
            EditorUtility.OpenPropertyEditor(description);
        }
    }
}