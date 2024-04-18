using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FPS
{
    public class CommandQueue
    {
        public delegate void ProgressDelegate(float progress);

        public event ProgressDelegate ProgressUpdateEvent;

        private readonly Queue<Command> _queue = new();
        private readonly CancellationTokenSource _cts = new();


        public void Enqueue(Command command)
        {
            _queue.Enqueue(command);
        }

        public async UniTaskVoid Execute()
        {
            var token = _cts.Token;
            float commandsCount = _queue.Count;
            while (_queue.Count > 0)
            {
                var command = _queue.Dequeue();
                switch (command)
                {
                    case SyncCommand syncCommand:
                        syncCommand.Do();
                        await UniTask.Yield();
                        break;

                    case AsyncCommand asyncCommand:
                        await asyncCommand.Do(token);
                        if (token.IsCancellationRequested)
                        {
                            Dispose();
                            return;
                        }
                        break;
                }
                Debug.Log($"{command.Name}: {command.Status}");
                ProgressUpdateEvent?.Invoke((commandsCount - _queue.Count) / commandsCount);
            }
            Dispose();
        }

        public void ClearSubscriptions()
        {
            ProgressUpdateEvent = null;
        }

        private void Dispose()
        {
            ClearSubscriptions();
            _cts.Dispose();
        }
    }
}