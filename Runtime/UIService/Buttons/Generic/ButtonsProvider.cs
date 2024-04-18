using System;
using System.Collections.Generic;
using EasyButtons;
using FPS;
using FPS.Buttons.ModularButton.Modules;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UIService.Buttons.Generic
{
    public class ButtonsProvider<T> : MonoBehaviour, IButtonsProvider<T>
    {
        [SerializeField] private bool autoClear = true;
        [SerializeField] private SerializableDictionary<T, ModularButton> buttons;

        private readonly HashSet<UnityAction> _subscriptions = new();


        public void Subscribe(Action<T> onClick)
        {
            foreach (var kvp in buttons)
            {
                _subscriptions.Add(OnClick);
                kvp.Value.OnClick.AddListener(OnClick);
                continue;

                void OnClick()
                {
                    onClick.Invoke(kvp.Key);
                }
            }
        }

        public bool TryGetModule<TModule>(T buttonID, out TModule module) where TModule : ButtonModule
        {
            if (TryGetButton(buttonID, out ModularButton button))
                return button.TryGetModule(out module);

            Debug.Log($"Module '{typeof(TModule).Name}' on button '{buttonID}' not found");
            module = null;
            return false;
        }

        public void Unsubscribe(Action<T> onClick)
        {
            foreach (var kvp in buttons)
            {
                if (!_subscriptions.Contains(OnClick))
                    continue;

                _subscriptions.Remove(OnClick);
                kvp.Value.OnClick.RemoveListener(OnClick);
                continue;

                void OnClick()
                {
                    onClick.Invoke(kvp.Key);
                }
            }
        }

        private bool TryGetButton(T buttonID, out ModularButton button)
        {
            if (buttons.TryGetValue(buttonID, out button))
                return true;

            Debug.Log($"Button '{buttonID}' not found");
            return false;
        }

        private void OnDisable()
        {
            if (autoClear)
                ClearSubscriptions();
        }

        public void ClearSubscriptions()
        {
            foreach (var button in buttons.Values)
            {
                foreach (var action in _subscriptions)
                {
                    button.OnClick.RemoveListener(action);
                }
            }
            _subscriptions.Clear();
        }


#if UNITY_EDITOR
        [Button]
        protected void FindButtons()
        {
            foreach (var button in GetComponentsInChildren<ModularButton>(true))
            {
                if (buttons.ContainsValue(button, out _))
                    continue;

                buttons.Add(default!, button);
            }
        }
#endif
    }
}