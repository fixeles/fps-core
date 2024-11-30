using System;
using System.Collections.Generic;
using FPS.Sheets;
using FPS.UI;
using VContainer;

namespace FPS
{
	public class BaseInitializationCommands
	{
		private IObjectResolver _resolver;
#if FPS_SHEETS
		private DTOStorage _dtoStorage;
#endif

		[Inject]
		public BaseInitializationCommands(
			IObjectResolver resolver,
#if FPS_SHEETS
			DTOStorage dtoStorage
#endif
		)
		{
			_resolver = resolver;
			_dtoStorage = dtoStorage;
		}

		public void Insert(CommandQueue commandQueue)
		{
			commandQueue.Enqueue(_resolver.Resolve<UIServiceInitCommand>());
			commandQueue.Enqueue(_resolver.Resolve<ShowLoaderCommand>().WithParams(commandQueue));
#if FPS_POOL
			commandQueue.Enqueue(_resolver.Resolve<FluffyPoolInitCommand>());
#endif
#if FPS_LOC
            commandQueue.Enqueue(new LocalizationInitCommand());
#endif

#if FPS_SHEETS
			var sheetData = new Dictionary<string, string>();
			commandQueue.Enqueue(new LoadSheetConfigCommand(sheetData));

			var derivedTypes = Utils.Reflection.FindAllDerivedTypes(typeof(ISheetDTO));
			foreach (var type in derivedTypes)
			{
				Type genericType = typeof(ParseSheetCommand<>).MakeGenericType(type);
				var command = (Command)Activator.CreateInstance(genericType, _dtoStorage, sheetData);
				commandQueue.Enqueue(command);
			}

			_dtoStorage = null;
#endif
			_resolver = null;
		}
	}
}