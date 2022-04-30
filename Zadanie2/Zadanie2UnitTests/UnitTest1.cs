using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

using Zadanie2;

namespace Zadanie2UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public class ConsoleRedirectionToStringWriter : IDisposable
        {
            private StringWriter stringWriter;
            private TextWriter originalOutput;

            public ConsoleRedirectionToStringWriter()
            {
                stringWriter = new StringWriter();
                originalOutput = Console.Out;
                Console.SetOut(stringWriter);
            }

            public string GetOutput()
            {
                return stringWriter.ToString();
            }

            public void Dispose()
            {
                Console.SetOut(originalOutput);
                stringWriter.Dispose();
            }
        }

        [TestClass]
        public class UnitTestMultiFunctionalDevice
        {
            [TestMethod]
            public void MultifunctionalDevice_StateOff()
            {
                var copier = new MultifunctionalDevice();
                copier.PowerOff();

                Assert.AreEqual(IDevice.State.off, copier.GetState());
            }
            [TestMethod]
            public void MultifunctionalDevice_StateOn()
            {
                var copier = new MultifunctionalDevice();
                copier.PowerOn();

                Assert.AreEqual(IDevice.State.on, copier.GetState());
            }

            [TestMethod]
            public void MultifunctionalDevice_Send_DeviceOn()
            {
                var device = new MultifunctionalDevice();
                device.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();

                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("aaa.pdf");
                    device.sendFax(doc1, "987654321");
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [TestMethod]
            public void Device_ScanAndSend_DeviceIsOff()
            {
                var copier = new MultifunctionalDevice();
                copier.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("abc.pdf");
                    copier.Print(in doc1);
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);

            }
            [TestMethod]
            public void MultifunctionalDevice_Scan_DeviceOn()
            {
                var copier = new MultifunctionalDevice();
                copier.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1;
                    copier.Scan(out doc1);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }
            [TestMethod]
            public void MultifunctionalDevice_Scan_DeviceOff()
            {
                var copier = new MultifunctionalDevice();
                copier.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1;
                    copier.Scan(out doc1);
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }
            [TestMethod]
            public void MultifunctionalDevice_Fax_DeviceOff()
            {
                var multifunctionalDevice = new MultifunctionalDevice();
                multifunctionalDevice.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("abc.pdf");
                    string a = "1234";
                    multifunctionalDevice.sendFax(doc1, a);
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }
            [TestMethod]
            public void MultifunctionalDevice_Fax_DeviceOn()
            {
                var multifunctionalDevice = new MultifunctionalDevice();
                multifunctionalDevice.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("aaa.pdf");
                    string a = "1234";
                    multifunctionalDevice.sendFax(doc1, a);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }
            [TestMethod]
            public void MultifunctionalDevice_ScanAndPrint_DeviceOn()
            {
                var copier = new MultifunctionalDevice();
                copier.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    copier.ScanAndPrint();
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }
            [TestMethod]
            public void MultifunctionalDevice_ScanAndPrint_DeviceOff()
            {
                var copier = new MultifunctionalDevice();
                copier.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    copier.ScanAndPrint();
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }
            [TestMethod]
            public void MultifunctionalDevice_ScanAndFax_DeviceOff()
            {
                var copier = new MultifunctionalDevice();
                copier.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    string a = "23111";
                    copier.ScanAndSend(a);
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }
            [TestMethod]
            public void MultifunctionalDevice_ScanAndFax_DeviceOn()
            {
                var copier = new MultifunctionalDevice();
                copier.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    string a = "4251";
                    copier.ScanAndSend(a);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }
            [TestMethod]
            public void MultifunctionalDevice_FaxCounter()
            {
                var copier = new MultifunctionalDevice();
                copier.PowerOn();
                string a = "2314";
                IDocument document1 = new PDFDocument("aaa.pdf");
                copier.sendFax(document1, a);
                IDocument document2 = new TextDocument("aaa.txt");
                copier.sendFax(document2, a);
                IDocument document3 = new ImageDocument("aaa.jpg");
                copier.sendFax(document3, a);
                IDocument document4 = new ImageDocument("aaa.jpg");
                copier.sendFax(document4, a);
                IDocument document5 = new ImageDocument("aaa.jpg");
                copier.sendFax(document5, a);
                IDocument document6 = new ImageDocument("aaa.jpg");
                copier.sendFax(document6, a);

                copier.PowerOff();
                copier.sendFax(document5, a);
                copier.Scan(out document1);
                copier.PowerOn();

                copier.ScanAndSend(a);
                copier.ScanAndSend(a);
                copier.ScanAndSend(a);
                copier.ScanAndSend(a);

                Assert.AreEqual(10, copier.FaxCounter);
            }
        }
    }
}
