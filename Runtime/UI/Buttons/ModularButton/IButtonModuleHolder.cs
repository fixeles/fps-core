﻿namespace FPS.UI.Buttons
{
    public interface IButtonModuleHolder
    {
        void AddModule<T>(T module) where T : ButtonModule;
        public bool TryGetModule<T>(out T module) where T : ButtonModule;
    }
}