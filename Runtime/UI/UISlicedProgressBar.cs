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

        public RectTransform Parent
        {
            set
            {
                _parentWidth = value.sizeDelta.x;
                transform.SetParent(value);
            }
        }

        public string Text { set => label.text = value; }

        public float FillAmount
        {
            set
            {
                value = Mathf.Clamp(value, 0f, 1f);
                var distance = _parentWidth * value - inset * 2;
                filler.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, inset, distance);
            }
        }
    }
}