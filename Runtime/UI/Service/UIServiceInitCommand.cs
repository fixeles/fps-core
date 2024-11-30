using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace FPS.UI
{
    public class UIServiceInitCommand : AsyncCommand
    {
        private readonly IUIService _uiService;
        
        [Inject]
        public UIServiceInitCommand(IUIService uiService)
        {
            _uiService = uiService;
        }

        public override async UniTask Do(CancellationToken token)
        {
            try
            {
                if (_uiService is not UIService uiService)
                    return;
                
                await uiService.InitAsync();
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