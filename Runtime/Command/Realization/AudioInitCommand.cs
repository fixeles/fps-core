using System;
using FPS.SFX;
using UnityEngine;

namespace FPS
{
    public class AudioInitCommand : SyncCommand
    {
        public override void Do()
        {
            try
            {
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