using UnityEngine;

namespace FPS
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