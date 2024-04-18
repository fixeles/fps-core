using TMPro;
using UnityEngine;

namespace FPS.UI
{
    public class UISlicedProgressBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private RectTransform filler;
        [SerializeField] private float inset = 3;

        private float _parentWidth;


        private void Start()
        {
            if (transform is RectTransform rectTransform)
                _parentWidth = rectTransform.rect.width;
        }

        private void SetParent(RectTransform parent)
        {
            _parentWidth = parent.sizeDelta.x;
            transform.SetParent(parent);
        }

        public void SetText(string content)
        {
            label.text = content;
        }

        public void Fill(float percent)
        {
            var distance = _parentWidth * percent - inset * 2;
            filler.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, inset, distance);
        }
    }
}