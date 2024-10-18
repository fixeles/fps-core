#if FPS_SHEETS
using System;
using System.Collections.Generic;

namespace FPS.Sheets
{
	public static class SheetCommands
	{
		public static void Insert(CommandQueue commandQueue, DTOStorage dtoStorage)
		{
			var sheetData = new Dictionary<string, string>();
			commandQueue.Enqueue(new LoadSheetConfigCommand(sheetData));

			var derivedTypes = Utils.Reflection.FindAllDerivedTypes(typeof(ISheetDTO));
			foreach (var type in derivedTypes)
			{
				Type genericType = typeof(ParseSheetCommand<>).MakeGenericType(type);
				var command = (Command)Activator.CreateInstance(genericType, dtoStorage, sheetData);
				commandQueue.Enqueue(command);
			}
		}
	}
}
#endif