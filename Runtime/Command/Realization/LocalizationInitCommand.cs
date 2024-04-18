#if FPS_LOC
using System;
using UnityEngine;

namespace FPS.Localization
{
    public class LocalizationInitCommand : SyncCommand
    {
        public override void Do()
        {
            try
            {
                FluffyLoc.Init();
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
#endif