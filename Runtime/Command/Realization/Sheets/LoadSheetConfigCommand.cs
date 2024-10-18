#if FPS_SHEETS
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace FPS.Sheets
{
	public class LoadSheetConfigCommand : AsyncCommand
	{
		private readonly Dictionary<string, string> _result;

		public LoadSheetConfigCommand(Dictionary<string, string> result)
		{
			_result = result;
		}

		public override async UniTask Do(CancellationToken token)
		{
#if UNITY_EDITOR
			try
			{
				var config = Resources.Load<SheetsConfig>(nameof(SheetsConfig));
				if (config.LoadType == SheetsConfig.SheetLoadType.EachSheet)
				{
					await SheetsApi.LoadEachDTO(_result);
					Status = CommandStatus.Success;
					return;
				}

				Resources.UnloadAsset(config);
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				Decode(GetCached());
				Status = CommandStatus.Success;
				return;
			}
#endif

			string encoded;
			try
			{
				encoded = await SheetsApi.LoadEncodedData();
			}
			catch (Exception e1)
			{
				Debug.LogException(e1);
				encoded = GetCached();
			}

			Decode(encoded);
			Status = CommandStatus.Success;
		}

		private void Decode(string encoded)
		{
			var decoded = GZip.Decode(encoded);
			var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(decoded);
			foreach (var kvp in deserialized)
				_result.Add(kvp.Key, kvp.Value);
		}

		private string GetCached()
		{
			try
			{
				var cachedData = Resources.Load<TextAsset>("CachedSheetsConfig");
				var encoded = cachedData.text;
				Resources.UnloadAsset(cachedData);
				return encoded;
			}
			catch (Exception e2)
			{
				Debug.LogException(e2);
				Status = CommandStatus.Error;
				throw;
			}
		}
	}
}
#endif