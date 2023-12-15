
namespace BlasII.ModdingAPI.Messages
{
    /// <summary>
    /// Provides access to communication between independent mods
    /// </summary>
    public class MessageHandler
    {
        private readonly BlasIIMod _mod;

        internal MessageHandler(BlasIIMod mod) => _mod = mod;

        public void Send(string receiver, string message, string[] args)
        {
            if (string.IsNullOrEmpty(message))
                return;

            Main.ModdingAPI.Log($"{_mod.Id} is sending message '{message}' to {receiver}");
            Main.ModLoader.ProcessModFunction(mod =>
            {
                if (mod.Id == receiver)
                    mod.MessageHandler.Receive(_mod.Id, message, args);
            });
        }

        public void Send(string receiver, string message) => Send(receiver, message, null);

        public void Broadcast(string message, string[] args)
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

        internal void Receive(string sender, string message, string[] args)
        {
            _mod.LogError("Received message from " + sender);
        }
    }
}
