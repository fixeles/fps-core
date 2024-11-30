using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FPS.UI
{
	public interface IUIService
	{
		public UniTask<T> Show<T>(WindowSwitchType switchType = WindowSwitchType.Fade,
			Transform parent = null) where T : class, IWindow;

		public UniTask<T> Show<T>(Type type,
			WindowSwitchType switchType = WindowSwitchType.Fade,
			Transform parent = null) where T : class, IWindow;

		public bool TryGet<T>(out T window) where T : class, IWindow;
		public void Hide<T>(WindowSwitchType switchType = WindowSwitchType.BounceWithFade) where T : IWindow;
		public void Hide(Type type, WindowSwitchType switchType = WindowSwitchType.BounceWithFade);
	}
}