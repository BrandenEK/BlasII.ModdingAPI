
namespace BlasII.ModdingAPI.Messages
{
    /// <summary>
    /// Provides access to communication between independent mods
    /// </summary>
    public class MessageHandler
    {
        private readonly BlasIIMod _mod;

        internal MessageHandler(BlasIIMod mod)
        {
            _mod = mod;
        }

        public void SendMessage(string message) => Main.ModLoader.SendMessage(this, new string[] { message });

        public void SendMessage(string[] message) => Main.ModLoader.SendMessage(this, message);

        protected internal virtual void ReceiveMessage(string sender, string[] message) { }

    }
}
