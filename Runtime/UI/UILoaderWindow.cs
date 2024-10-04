using UnityEngine;

namespace FPS.UI
{
    public class UILoaderWindow : UIWindow
    {
        [SerializeField] private UISlicedProgressBar progressBar;

        public void UpdateProgress(float percent, string labelContent)
        {
            progressBar.Text = labelContent;
            progressBar.FillAmount = percent;
        }
    }
}