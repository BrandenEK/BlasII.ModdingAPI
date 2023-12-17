using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlasII.ModdingAPI
{
    internal class TestMod : BlasIIMod
    {
        public TestMod() : base("BlasII.Test", "Test Mod", "Damocles", "1.0.0") { }

        protected internal override void OnInitialize()
        {
            MessageHandler.AllowReceivingBroadcasts = true;
            MessageHandler.AddGlobalListener((mod, message, content) =>
            {
                LogWarning($"Test: Received {message} from {mod} with {content}");
            });
            MessageHandler.AddMessageListener("BlasII.ModdingAPI", "Special", PerformSpecial);
            MessageHandler.AddMessageListener("BlasII.ModdingAPI", "Kill", enemy =>
            {
                LogError("Killed a " + enemy);
            });
            MessageHandler.AddModListener("BlasII.ModdingAPI", (_, _) =>
            {
                LogWarning("Received message from api");
            });
        }

        private void PerformSpecial(string obj)
        {
            LogError("Recevied special object: " + obj);
            throw new Exception("test error");
        }
    }
}
