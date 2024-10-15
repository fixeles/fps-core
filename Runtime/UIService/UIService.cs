using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace FPS
{
	public static class UIService
	{
		private static readonly Dictionary<Type, UIWindow> ActiveWindows = new();
		private static readonly Dictionary<Type, IResourceLocation> WindowsLocations = new();
		private static readonly HashSet<Type> InProgress = new();

		private static UIRoot _root;

		public static async UniTask InitAsync()
		{
			var rootAsset = await Addressables.InstantiateAsync(nameof(UIRoot)).Task;
			_root = rootAsset.GetComponent<UIRoot>();

			var locations = await Addressables.LoadResourceLocationsAsync("Window").Task;
			foreach (var location in locations)
			{
				var asset = await Addressables.LoadAssetAsync<GameObject>(location).Task;
				WindowsLocations.Add(asset.GetComponent<UIWindow>().GetType(), location);
				Addressables.Release(asset);
			}

			Addressables.Release(locations);
		}

		public static async UniTask<T> Show<T>(WindowSwitchType switchType = WindowSwitchType.Fade,
			Transform parent = null) where T : class, IWindow
		{
			return await Show<T>(typeof(T), switchType, parent);
		}

		public static async UniTask<T> Show<T>(Type type, WindowSwitchType switchType = WindowSwitchType.Fade,
			Transform parent = null) where T : class, IWindow
		{
			if (InProgress.Contains(type))
				return null;

			if (ActiveWindows.TryGetValue(type, out var activeWindow))
				return activeWindow as T;

			InProgress.Add(type);
			_root.SwitchBlock(true);
			var go = await Addressables.InstantiateAsync(WindowsLocations[type]).Task;
			var window = go.GetComponent<UIWindow>();
			window.Show(switchType);
			ActiveWindows.Add(type, window);
			InProgress.Remove(type);

			parent ??= _root.WindowsContainer;
			window.Transform.SetParent(parent, false);
			_root.SwitchBlock(false);
			return window as T;
		}

		public static bool TryGet<T>(out T window) where T : class, IWindow
		{
			var type = typeof(T);
			if (!ActiveWindows.ContainsKey(type))
			{
				window = null;
				return false;
			}

			window = ActiveWindows[type] as T;
			return true;
		}

		public static void Hide<T>(WindowSwitchType switchType = WindowSwitchType.BounceWithFade) where T : IWindow
		{
			Hide<T>(typeof(T), switchType);
		}

		public static void Hide<T>(Type type, WindowSwitchType switchType = WindowSwitchType.BounceWithFade)
			where T : IWindow
		{
			var window = ActiveWindows[type];
			window.Hide(switchType);
			window.Transform.SetParent(_root.transform);
			Addressables.ReleaseInstance(window.gameObject);
		}
	}
}