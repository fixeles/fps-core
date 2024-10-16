using UnityEngine;

namespace FPS.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private RectTransform windowsContainer;
        [SerializeField] private GameObject block;
        
        
        public Transform WindowsContainer => windowsContainer;

        public void SwitchBlock(bool isActive)
        {
            block.SetActive(isActive);
        }
    }
}