#if FPS_SHEETS
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Sheets
{
	public class ParseSheetCommand<T> : SyncCommand where T : ISheetDTO
	{
		private readonly SheetsApi _sheetsApi;
		private readonly Dictionary<string, string> _decodedData;

		public ParseSheetCommand(SheetsApi sheetsApi, Dictionary<string, string> decodedData)
		{
			_sheetsApi = sheetsApi;
			_decodedData = decodedData;
		}

		public override string Name => $"ParseSheetCommand:{typeof(T).Name}";

		public override void Do()
		{
			try
			{
				_sheetsApi.Parse<T>(_decodedData[typeof(T).Name]);
				Status = CommandStatus.Success;
			}
			catch (Exception e)
			{
				Status = CommandStatus.Error;
				Debug.LogException(e);
				throw;
			}
		}
	}
}
#endif