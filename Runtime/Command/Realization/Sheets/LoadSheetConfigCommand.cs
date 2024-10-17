#if FPS_SHEETS
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using FPS.Sheets.Editor;
using Newtonsoft.Json;
using UnityEngine;

namespace FPS.Sheets
{
	public class LoadSheetConfigCommand : AsyncCommand
	{
		private readonly SheetsApi _sheetsApi;
		private readonly Dictionary<string, string> _result;

		public LoadSheetConfigCommand(SheetsApi sheetsApi, Dictionary<string, string> result)
		{
			_sheetsApi = sheetsApi;
			_result = result;
		}

		public override async UniTask Do(CancellationToken token)
		{
			string compressed;
#if UNITY_EDITOR
			try
			{
				var config = Resources.Load<SheetsConfig>(nameof(SheetsConfig));
				if (config.ReceiveType == SheetsConfig.SheetLoadType.EachList)
					await CompressedConfig.GenerateAsync();

				var cachedData = Resources.Load<TextAsset>("CachedSheetsData");
				compressed = cachedData.text;
				Resources.UnloadAsset(cachedData);
			}
			catch (System.Exception e)
			{
				Status = CommandStatus.Error;
				Debug.LogException(e);
				throw;
			}
#else
			try
			{
				compressed = await _sheetsApi.LoadCompressedData();
			}
			catch (System.Exception e)
			{
				var cachedData = Resources.Load<TextAsset>("CachedSheetsData");
				compressed = cachedData.text;
				Resources.UnloadAsset(cachedData);
				Debug.LogException(e);
				Status = CommandStatus.Error;
			}
#endif
			var decoded = GZip.Decode(compressed);
			var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(decoded);
			foreach (var kvp in deserialized)
				_result.Add(kvp.Key, kvp.Value);

			Status = CommandStatus.Success;
		}
	}
}
#endif