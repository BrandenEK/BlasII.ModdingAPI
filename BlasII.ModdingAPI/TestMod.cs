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
            MessageHandler.AddGlobalListener((string x, string y) =>
            {
                LogWarning("Test: Received global message");
            });
        }
    }
}
