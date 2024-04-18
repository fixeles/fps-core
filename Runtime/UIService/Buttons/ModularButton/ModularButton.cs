using System;
using System.Collections.Generic;
using FPS;
using FPS.Buttons.ModularButton.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UIService.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ModularButton : MonoBehaviour, IButtonModuleHolder
    {
        private readonly Dictionary<Type, ButtonModule> _modules = new();

        [SerializeField, Get] private Button button;

        public Button.ButtonClickedEvent OnClick => button.onClick;

        public void AddModule<T>(T module) where T : ButtonModule
        {
            var type = module.GetType();
            _modules.TryAdd(type, module);
        }

        public bool TryGetModule<T>(out T module) where T : ButtonModule
        {
            var hasModule = _modules.ContainsKey(typeof(T));
            module = hasModule ? _modules[typeof(T)] as T : null;
            return hasModule;
        }

        private void Awake()
        {
            var components = GetComponents<ButtonModule>();
            foreach (var module in components)
            {
                AddModule(module);
            }
        }
    }
}