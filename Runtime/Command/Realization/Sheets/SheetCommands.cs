#if FPS_SHEETS
using System;
using System.Collections.Generic;

namespace FPS.Sheets
{
	public static class SheetCommands
	{
		public static void Insert(CommandQueue commandQueue, SheetsApi sheetsApi)
		{
			var sheetData = new Dictionary<string, string>();
			commandQueue.Enqueue(new LoadSheetConfigCommand(sheetsApi, sheetData));

			var derivedTypes = Utils.Reflection.FindAllDerivedTypes(typeof(ISheetDTO));
			foreach (var type in derivedTypes)
			{
				Type genericType = typeof(ParseSheetCommand<>).MakeGenericType(type);
				var command = (Command)Activator.CreateInstance(genericType, sheetsApi, sheetData);
				commandQueue.Enqueue(command);
			}
		}
	}
}
#endif
