
namespace BlasII.ModdingAPI.Messages
{
    /// <summary>
    /// Provides access to communication between independent mods
    /// </summary>
    public class MessageHandler
    {
        private readonly BlasIIMod _mod;

        internal MessageHandler(BlasIIMod mod) => _mod = mod;

        public void SendMessage(string message) => SendMessage(message, null);

        public void SendMessage(string message, string[] args)
        {
            if (string.IsNullOrEmpty(message))
                return;

            Main.ModdingAPI.Log($"{_mod.Name} is sending {message}.");
            Main.ModLoader.ProcessModFunction(mod =>
            {
                if (mod != _mod)
                    mod.MessageHandler.ReceiveMessage(_mod.Id, message, args);
            });
        }

        internal void ReceiveMessage(string sender, string message, string[] args)
        {
            _mod.LogError("Received message from " + sender);
        }
    }
}
