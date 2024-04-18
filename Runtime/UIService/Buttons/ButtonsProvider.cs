using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using EasyButtons;
using FPS;
using FPS.Buttons.ModularButton.Modules;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UIService.Buttons
{
    public class ButtonsProvider : MonoBehaviour, IButtonsProvider
    {
        private readonly ConcurrentDictionary<string, HashSet<UnityAction>> _subscriptions = new();

        [SerializeField] private SerializableDictionary<string, ModularButton> buttons = new();


        public void Subscribe(string buttonID, Action onClick)
        {
            _subscriptions.TryAdd(buttonID, new());
            _subscriptions[buttonID].Add(onClick.Invoke);

            if (TryGetButton(buttonID, out var button))
                button.OnClick.AddListener(onClick.Invoke);
        }

        public void Unsubscribe(string buttonID, Action onClick)
        {
            if (!_subscriptions.ContainsKey(buttonID))
                return;

            if (!_subscriptions[buttonID].Contains(onClick.Invoke))
                return;

            _subscriptions[buttonID].Remove(onClick.Invoke);
            if (TryGetButton(buttonID, out var button))
                button.OnClick.RemoveListener(onClick.Invoke);
        }

        public bool TryGetModule<T>(string buttonID, out T module) where T : ButtonModule
        {
            if (TryGetButton(buttonID, out ModularButton button))
                return button.TryGetModule(out module);

            Debug.Log($"Module '{typeof(T).Name}' on button '{buttonID}' not found");
            module = null;
            return false;
        }

        private bool TryGetButton(string buttonID, out ModularButton button)
        {
            if (buttons.TryGetValue(buttonID, out button))
                return true;

            Debug.Log($"Button '{buttonID}' not found");
            return false;
        }

        private void OnDisable()
        {
            ClearSubscriptions();
        }

        public void ClearSubscriptions()
        {
            foreach (var kvp in buttons)
            {
                if (_subscriptions.TryGetValue(kvp.Key, out HashSet<UnityAction> actions))
                {
                    foreach (var action in actions)
                    {
                        kvp.Value.OnClick.RemoveListener(action);
                    }
                }
                _subscriptions.TryRemove(kvp.Key, out _);
            }
        }

#if UNITY_EDITOR
        [Button]
        private void FindButtons()
        {
            foreach (var button in GetComponentsInChildren<ModularButton>(true))
            {
                if (buttons.ContainsValue(button, out _))
                    continue;
                buttons.Add(button.gameObject.name, button);
            }
        }
#endif
    }
}