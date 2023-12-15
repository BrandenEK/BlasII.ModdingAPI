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

        private readonly List<GlobalListener> _globalListeners = new();
        private readonly List<ModListener> _modListeners = new();
        private readonly List<MessageListener> _messageListeners = new();
        private readonly List<ContentListener> _contentListeners = new();

        internal MessageHandler(BlasIIMod mod) => _mod = mod;

        // Sending messages

        public void Send(string receiver, string message, string content)
        {
            if (string.IsNullOrEmpty(message) || receiver == _mod.Id)
                return;

            Main.ModdingAPI.Log($"{_mod.Id} is sending message '{message}' to {receiver}");
            Main.ModLoader.ProcessModFunction(mod =>
            {
                if (mod.Id == receiver)
                    mod.MessageHandler.Receive(_mod.Id, message, content ?? string.Empty);
            });
        }

        public void Send(string receiver, string message) => Send(receiver, message, null);

        // Broadcasting messages

        public void Broadcast(string message, string content)
        {
            if (string.IsNullOrEmpty(message))
                return;

            Main.ModdingAPI.Log($"{_mod.Id} is broadcasting message '{message}'");
            Main.ModLoader.ProcessModFunction(mod =>
            {
                if (mod != _mod)
                    mod.MessageHandler.Receive(_mod.Id, message, content ?? string.Empty);
            });
        }

        public void Broadcast(string message) => Broadcast(message, null);

        // Receiving messages

        internal void Receive(string sender, string message, string content)
        {
            _mod.LogError("Received message from " + sender);

            foreach (var listener in _globalListeners)
                listener.callback(sender, message, content);

            foreach (var listener in _modListeners.Where(x => x.mod == sender))
                listener.callback(message, content);

            foreach (var listener in _messageListeners.Where(x => x.mod == sender && x.message == message))
                listener.callback(content);

            foreach (var listener in _contentListeners.Where(x => x.mod == sender && x.message == message && x.content == content))
                listener.callback();
        }

        public void AddGlobalListener(Action<string, string, string> callback) =>
            _globalListeners.Add(new GlobalListener(callback));

        public void AddModListener(string mod, Action<string, string> callback) =>
            _modListeners.Add(new ModListener(mod, callback));

        public void AddMessageListener(string mod, string message, Action<string> callback) =>
            _messageListeners.Add(new MessageListener(mod, message, callback));

        public void AddContentListener(string mod, string message, string content, Action callback) =>
            _contentListeners.Add(new ContentListener(mod, message, content, callback));
    }
}
