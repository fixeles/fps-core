#if FPS_SHEETS
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Sheets
{
	public class ParseSheetCommand<T> : SyncCommand where T : ISheetDTO
	{
		private readonly DTOStorage _dtoStorage;
		private readonly Dictionary<string, string> _decodedData;

		public ParseSheetCommand(DTOStorage dtoStorage, Dictionary<string, string> decodedData)
		{
			_dtoStorage = dtoStorage;
			_decodedData = decodedData;
		}

		public override string Name => $"ParseSheetCommand:{typeof(T).Name}";

		public override void Do()
		{
			try
			{
				Parser.ParseSheet<T>(_dtoStorage, _decodedData[typeof(T).Name]);
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