using System;
using System.Collections.Generic;
using System.Linq;

namespace BlasII.ModdingAPI.Messages
{
    /// <summary>
    /// Provides access to communication between independent mods
    /// </summary>
    public class MessageHandler
    {
        private readonly BlasIIMod _mod;

        private readonly List<IListener> _listeners = new();

        internal MessageHandler(BlasIIMod mod) => _mod = mod;

        public bool AllowReceivingBroadcasts { get; set; }

        // Sending messages

        public void Send(string receiver, string message, string content)
        {
            if (string.IsNullOrEmpty(message) || receiver == _mod.Id)
                return;

            Main.ModdingAPI.Log($"{_mod.Id} is sending message '{message}' [{content}] to {receiver}");
            if (_mod.IsModLoaded(receiver, out BlasIIMod mod))
            {
                mod.MessageHandler.Receive(_mod.Id, message, content ?? string.Empty);
            }
        }

        public void Send(string receiver, string message) => Send(receiver, message, null);

        // Broadcasting messages

        public void Broadcast(string message, string content)
        {
            if (string.IsNullOrEmpty(message))
                return;

            Main.ModdingAPI.Log($"{_mod.Id} is broadcasting message '{message}' [{content}]");
            Main.ModLoader.ProcessModFunction(mod =>
            {
                if (mod != _mod && mod.MessageHandler.AllowReceivingBroadcasts)
                    mod.MessageHandler.Receive(_mod.Id, message, content ?? string.Empty);
            });
        }

        public void Broadcast(string message) => Broadcast(message, null);

        // Receiving messages

        internal void Receive(string sender, string message, string content)
        {
            _mod.LogError("Received message from " + sender);

            try
            {
                foreach (var listener in _listeners)
                    listener.OnReceive(sender, message, content);
            }
            catch
            {
                _mod.LogError($"Failed to receive message '{message}' from {sender}");
            }
        }

        public void AddGlobalListener(Action<string, string, string> callback) =>
            _listeners.Add(new GlobalListener(callback));

        public void AddModListener(string mod, Action<string, string> callback) =>
            _listeners.Add(new ModListener(mod, callback));

        public void AddMessageListener(string mod, string message, Action<string> callback) =>
            _listeners.Add(new MessageListener(mod, message, callback));

        public void AddContentListener(string mod, string message, string content, Action callback) =>
            _listeners.Add(new ContentListener(mod, message, content, callback));
    }
}
