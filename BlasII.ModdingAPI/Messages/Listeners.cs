using System;

namespace BlasII.ModdingAPI.Messages
{
    internal class GlobalListener
    {
        public readonly Action<string, string, string> callback;

        public GlobalListener(Action<string, string, string> callback)
        {
            this.callback = callback;
        }
    }

    internal class ModListener
    {
        public readonly string mod;
        public readonly Action<string, string> callback;

        public ModListener(string mod, Action<string, string> callback)
        {
            this.mod = mod;
            this.callback = callback;
        }
    }

    internal class MessageListener
    {
        public readonly string mod;
        public readonly string message;
        public readonly Action<string> callback;

        public MessageListener(string mod, string message, Action<string> callback)
        {
            this.mod = mod;
            this.message = message;
            this.callback = callback;
        }
    }

    internal class ContentListener
    {
        public readonly string mod;
        public readonly string message;
        public readonly string content;
        public readonly Action callback;

        public ContentListener(string mod, string message, string content, Action callback)
        {
            this.mod = mod;
            this.message = message;
            this.content = content;
            this.callback = callback;
        }
    }
}
