using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlasII.ModdingAPI.Tests
{
    [TestClass]
    public class ModTests
    {
        internal static TestMod TestMod { get; private set; }

        [AssemblyInitialize]
        public static void CreateMod(TestContext _)
        {
            var main = new Main();
            main.OnInitializeMelon();

            TestMod = new TestMod();
        }

        [TestMethod]
        public void AreModsLoaded()
        {
            Assert.IsTrue(TestMod.IsModLoaded("BlasII.ModdingAPI", out _)
                && TestMod.IsModLoaded("BlasII.Test", out _));
        }
    }
}