using BlasII.ModdingAPI.Helpers;
using System;
using System.Collections.Generic;

namespace BlasII.ModdingAPI.Messages;

/// <summary>
/// Provides access to communication between independent mods
/// </summary>
public class MessageHandler
{
    private readonly BlasIIMod _mod;

    private readonly List<IListener> _listeners = new();

    internal MessageHandler(BlasIIMod mod) => _mod = mod;

    /// <summary>
    /// Whether this mod will listen for broadcasts instead of only direct messages
    /// </summary>
    public bool AllowReceivingBroadcasts { get; set; }

    // Sending messages

    /// <summary>
    /// Sends a message with content to the specified mod
    /// </summary>
    public void Send(string receiver, string message, string content)
    {
        if (string.IsNullOrEmpty(message) || receiver == _mod.Id)
            return;

        ModLog.Info($"Sending message '{message}' [{content}] to {receiver}", _mod);
        if (ModHelper.TryGetModById(receiver, out BlasIIMod mod))
        {
            mod.MessageHandler.Receive(_mod.Id, message, content ?? string.Empty);
        }
    }

    /// <summary>
    /// Sends a message without content to the specified mod
    /// </summary>
    public void Send(string receiver, string message) => Send(receiver, message, null);

    // Broadcasting messages

    /// <summary>
    /// Broadcasts a message with content to all mods
    /// </summary>
    public void Broadcast(string message, string content)
    {
        if (string.IsNullOrEmpty(message))
            return;

        ModLog.Info($"Broadcasting message '{message}' [{content}]", _mod);
        Main.ModLoader.ProcessModFunction(mod =>
        {
            if (mod != _mod && mod.MessageHandler.AllowReceivingBroadcasts)
                mod.MessageHandler.Receive(_mod.Id, message, content ?? string.Empty);
        });
    }

    /// <summary>
    /// Broadcasts a message without content to all mods
    /// </summary>
    public void Broadcast(string message) => Broadcast(message, null);

    // Receiving messages

    /// <summary>
    /// Receives a message and allows its listeners to process it
    /// </summary>
    internal void Receive(string sender, string message, string content)
    {
        try
        {
            foreach (var listener in _listeners)
                listener.OnReceive(sender, message, content);
        }
        catch
        {
            ModLog.Error($"Failed to receive message '{message}' from {sender}", _mod);
        }
    }

    /// <summary>
    /// Listens for any and all messages
    /// </summary>
    public void AddGlobalListener(Action<string, string, string> callback) =>
        _listeners.Add(new GlobalListener(callback));

    /// <summary>
    /// Listens for messages from a certain mod
    /// </summary>
    public void AddModListener(string mod, Action<string, string> callback) =>
        _listeners.Add(new ModListener(mod, callback));

    /// <summary>
    /// Listens for messages from a certain mod with a certain message
    /// </summary>
    public void AddMessageListener(string mod, string message, Action<string> callback) =>
        _listeners.Add(new MessageListener(mod, message, callback));

    /// <summary>
    /// Listens for messages from a certain mod with a certain message and content
    /// </summary>
    public void AddContentListener(string mod, string message, string content, Action callback) =>
        _listeners.Add(new ContentListener(mod, message, content, callback));
}
