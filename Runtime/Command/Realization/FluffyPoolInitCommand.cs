#if FPS_POOL
using System;
using FPS.Pool;
using UnityEngine;
using VContainer;

namespace FPS
{
	public class FluffyPoolInitCommand : SyncCommand
	{
		private readonly RuntimeDispatcher _runtimeDispatcher;
		private readonly FluffyPool _pool;

		[Inject]
		public FluffyPoolInitCommand(IObjectPool pool, RuntimeDispatcher runtimeDispatcher)
		{
			_runtimeDispatcher = runtimeDispatcher;
			if (pool is FluffyPool fluffyPool)
				_pool = fluffyPool;
		}

		public override void Do()
		{
			try
			{
				_pool.Init(_runtimeDispatcher.CancellationToken);
				Status = CommandStatus.Success;
			}
			catch (Exception e)
			{
				Status = CommandStatus.Error;
				Debug.LogError(e);
			}
		}
	}
}

#endif