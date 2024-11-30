using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace FPS.UI
{
	public class UIService : IUIService
	{
		private readonly Dictionary<Type, UIWindow> _activeWindows = new();
		private readonly Dictionary<Type, IResourceLocation> _windowsLocations = new();
		private readonly HashSet<Type> _inProgress = new();

		private UIRoot _root;

		public async UniTask InitAsync()
		{
			var rootAsset = await Addressables.InstantiateAsync(nameof(UIRoot)).Task;
			_root = rootAsset.GetComponent<UIRoot>();

			var locations = await Addressables.LoadResourceLocationsAsync("Window").Task;
			foreach (var location in locations)
			{
				var asset = await Addressables.LoadAssetAsync<GameObject>(location).Task;
				_windowsLocations.Add(asset.GetComponent<UIWindow>().GetType(), location);
				Addressables.Release(asset);
			}

			Addressables.Release(locations);
		}

		public async UniTask<T> Show<T>(WindowSwitchType switchType = WindowSwitchType.Fade,
			Transform parent = null) where T : class, IWindow
		{
			return await Show<T>(typeof(T), switchType, parent);
		}

		public async UniTask<T> Show<T>(Type type, WindowSwitchType switchType = WindowSwitchType.Fade,
			Transform parent = null) where T : class, IWindow
		{
			if (_inProgress.Contains(type))
				return null;

			if (_activeWindows.TryGetValue(type, out var activeWindow))
				return activeWindow as T;

			_inProgress.Add(type);
			_root.SwitchBlock(true);
			var go = await Addressables.InstantiateAsync(_windowsLocations[type]).Task;
			var window = go.GetComponent<UIWindow>();
			window.Show(switchType);
			_activeWindows.Add(type, window);
			_inProgress.Remove(type);

			parent ??= _root.WindowsContainer;
			window.Transform.SetParent(parent, false);
			_root.SwitchBlock(false);
			return window as T;
		}

		public bool TryGet<T>(out T window) where T : class, IWindow
		{
			var type = typeof(T);
			if (!_activeWindows.ContainsKey(type))
			{
				window = null;
				return false;
			}

			window = _activeWindows[type] as T;
			return true;
		}

		public void Hide<T>(WindowSwitchType switchType = WindowSwitchType.BounceWithFade) where T : IWindow
		{
			Hide(typeof(T), switchType);
		}

		public void Hide<T>(T window, WindowSwitchType switchType = WindowSwitchType.BounceWithFade)
			where T : IWindow
		{
			Hide(typeof(T), switchType);
		}

		public void Hide(Type type, WindowSwitchType switchType = WindowSwitchType.BounceWithFade)
		{
			var window = _activeWindows[type];
			window.Hide(switchType);
			window.Transform.SetParent(_root.transform);
			Addressables.ReleaseInstance(window.gameObject);
		}
	}
}