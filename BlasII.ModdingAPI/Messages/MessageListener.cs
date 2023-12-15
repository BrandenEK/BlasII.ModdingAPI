using System;

namespace BlasII.ModdingAPI.Messages
{
    internal class MessageListener
    {
        public readonly string sender;
        public readonly string message;
        public readonly Action<string, string> callback;

        public MessageListener(string sender, string message, Action<string, string> callback)
        {
            this.sender = sender;
            this.message = message;
            this.callback = callback;
        }
    }
}
