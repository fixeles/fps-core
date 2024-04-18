using System;
using FPS.Buttons.ModularButton.Modules;

namespace Game.UIService.Buttons.Generic
{
    public interface IButtonsProvider<T>
    {
        void Subscribe(Action<T> onClick);
        void Unsubscribe(Action<T> onClick);
        void ClearSubscriptions();
        bool TryGetModule<TModule>(T buttonID, out TModule module) where TModule : ButtonModule;
    }
}