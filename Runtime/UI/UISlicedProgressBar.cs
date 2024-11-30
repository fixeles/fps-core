using TMPro;
using UnityEngine;

namespace FPS.UI
{
	public class UISlicedProgressBar : MonoBehaviour
	{
		[SerializeField] private TMP_Text label;
		[SerializeField] private RectTransform filler;
		[SerializeField] private float inset = 3;

		public RectTransform Parent
		{
			set => transform.SetParent(value, false);
		}

		public string Text
		{
			set => label.text = value;
		}

		public float FillAmount
		{
			set
			{
				if (transform.parent is not RectTransform parent)
					return;

				value = Mathf.Clamp(value, 0f, 1f);
				var distance = parent.rect.width * value - inset * 2;
				filler.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, inset, distance);
			}
		}
	}
}