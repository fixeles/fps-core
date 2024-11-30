using System;
using FPS.Pool;
using FPS.SFX;
using FPS.Sheets;
using FPS.UI;
using VContainer;
using VContainer.Unity;

namespace FPS.DI
{
	public class AppScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<DTOStorage>(Lifetime.Singleton);
			builder.RegisterComponentOnNewGameObject<RuntimeDispatcher>(Lifetime.Singleton);
			builder.Register<FluffyAudio>(Lifetime.Singleton).As<IAudioService>();
			builder.Register<UIService>(Lifetime.Singleton).As<IUIService>();
#if FPS_POOL
			builder.Register<FluffyPool>(Lifetime.Singleton).As<IObjectPool>();
#endif
			AutoRegister(builder);
		}

		private static void AutoRegister(IContainerBuilder builder)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (var assembly in assemblies)
			foreach (var type in assembly.GetTypes())
			{
				if (builder.Exists(type))
					continue;

				if (type.HasInjections())
					builder.Register(type, Lifetime.Transient);
			}
		}
	}
}