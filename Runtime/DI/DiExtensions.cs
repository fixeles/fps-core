using System;
using System.Linq;
using VContainer;

namespace FPS.DI
{
	public static class DiExtensions
	{
		public static bool HasInjections(this Type type)
		{
			var hasFieldInjects = type.GetFields().SelectMany(f => f.GetCustomAttributes(false))
				.OfType<InjectAttribute>().Any();

			var hasConstructorInjects = type.GetConstructors().SelectMany(f => f.GetCustomAttributes(false))
				.OfType<InjectAttribute>().Any();

			var hasMethodInjects = type.GetMethods().SelectMany(m => m.GetCustomAttributes(false))
				.OfType<InjectAttribute>().Any();

			return hasFieldInjects || hasConstructorInjects || hasMethodInjects;
		}
	}
}