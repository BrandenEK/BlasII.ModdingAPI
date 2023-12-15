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

        private readonly List<MessageListener> _listeners = new();

        internal MessageHandler(BlasIIMod mod) => _mod = mod;

        // Sending messages

        public void Send(string receiver, string message, string args)
        {
            if (string.IsNullOrEmpty(message) || receiver == _mod.Id)
                return;

            Main.ModdingAPI.Log($"{_mod.Id} is sending message '{message}' to {receiver}");
            Main.ModLoader.ProcessModFunction(mod =>
            {
                if (mod.Id == receiver)
                    mod.MessageHandler.Receive(_mod.Id, message, args);
            });
        }

        public void Send(string receiver, string message) => Send(receiver, message, null);

        // Broadcasting messages

        public void Broadcast(string message, string args)
        {
            if (string.IsNullOrEmpty(message))
                return;

            Main.ModdingAPI.Log($"{_mod.Id} is broadcasting message '{message}'");
            Main.ModLoader.ProcessModFunction(mod =>
            {
                if (mod != _mod)
                    mod.MessageHandler.Receive(_mod.Id, message, args);
            });
        }

        public void Broadcast(string message) => Broadcast(message, null);

        // Receiving messages

        internal void Receive(string sender, string message, string args)
        {
            _mod.LogError("Received message from " + sender);

            foreach (var listener in _listeners.Where(x =>
                (x.sender == "any" || x.sender == sender) && (x.message == "any" | x.message == message)))
            {
                listener.callback(sender, args);
            }
        }

        public void AddGlobalListener(Action<string, string> callback)
        {
            _listeners.Add(new MessageListener("any", "any", callback));
        }
    }
}
