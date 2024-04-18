using System;
using FPS.Buttons.ModularButton.Modules;

namespace Game.UIService.Buttons
{
    public interface IButtonsProvider
    {
        void Subscribe(string buttonID, Action onClick);
        void Unsubscribe(string buttonID, Action onClick);
        void ClearSubscriptions();
        bool TryGetModule<T>(string buttonID, out T module) where T : ButtonModule;
    }
}