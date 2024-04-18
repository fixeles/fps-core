#if FPS_POOL 
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FPS.Pool
{
    public class PoolInitCommand : AsyncCommand
    {
        public override async UniTask Do(CancellationToken token)
        {
            try
            {
                await FluffyPool.InitAsync();
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