using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonTasksLib.UnSorted;
using System.Threading;

namespace LibTests
{
    [TestClass]
    public class ThreadingTest
    {
        [TestMethod]
        public void RunAsynchronous_Default()
        {
            Boolean first = false, second = false;
            this.RunAsynchronously(() =>
            {
                Console.WriteLine("First Method started => {0}", DateTime.Now);
                Thread.Sleep(5000);
                Console.WriteLine("First Method ended => {0}", DateTime.Now);
            }, () =>
            {
                first = true;
            });
            Thread.Sleep(1000);
            this.RunAsynchronously(() =>
            {
                Console.WriteLine("Second Method started => {0}", DateTime.Now);
                Thread.Sleep(5000);
                Console.WriteLine("Second Method ended => {0}", DateTime.Now);
            }, () =>
            {
                second = true;
            });

            while (!first || !second)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Waiting threads to complete");
            }

            Assert.IsTrue(true);
        }
    }
}
