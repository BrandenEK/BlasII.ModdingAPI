using System;

namespace BlasII.ModdingAPI.Messages;

internal interface IListener
{
    public void OnReceive(string mod, string message, string content);
}

internal class GlobalListener : IListener
{
    private readonly Action<string, string, string> callback;

    public GlobalListener(Action<string, string, string> callback)
    {
        this.callback = callback;
    }

    public void OnReceive(string mod, string message, string content)
    {
        callback(mod, message, content);
    }
}

internal class ModListener : IListener
{
    private readonly string mod;
    private readonly Action<string, string> callback;

    public ModListener(string mod, Action<string, string> callback)
    {
        this.mod = mod;
        this.callback = callback;
    }

    public void OnReceive(string mod, string message, string content)
    {
        if (this.mod == mod)
            callback(message, content);
    }
}

internal class MessageListener : IListener
{
    private readonly string mod;
    private readonly string message;
    private readonly Action<string> callback;

    public MessageListener(string mod, string message, Action<string> callback)
    {
        this.mod = mod;
        this.message = message;
        this.callback = callback;
    }

    public void OnReceive(string mod, string message, string content)
    {
        if (this.mod == mod && this.message == message)
            callback(content);
    }
}

internal class ContentListener : IListener
{
    private readonly string mod;
    private readonly string message;
    private readonly string content;
    private readonly Action callback;

    public ContentListener(string mod, string message, string content, Action callback)
    {
        this.mod = mod;
        this.message = message;
        this.content = content;
        this.callback = callback;
    }

    public void OnReceive(string mod, string message, string content)
    {
        if (this.mod == mod && this.message == message && this.content == content)
            callback();
    }
}
