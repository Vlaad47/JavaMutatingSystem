using System;
using JavaMag;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JavaMagTests
{
    [TestClass]
    public class SwitchTest
    {
        [TestMethod]
        public void SwitchProducesRightOutput()
        {
            Console.WriteLine(new SwitchBuilder("mutator1").AddDefaultCase("a += 1;").AddEnd().ToString());
            Console.Read();
        }
    }
}
