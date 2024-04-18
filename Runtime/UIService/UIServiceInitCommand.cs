using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FPS
{
    public class UIServiceInitCommand : AsyncCommand
    {
        public override async UniTask Do(CancellationToken token)
        {
            try
            {
                await UIService.InitAsync();
                Status = CommandStatus.Success;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                Status = CommandStatus.Error;
            }
        }
    }
}