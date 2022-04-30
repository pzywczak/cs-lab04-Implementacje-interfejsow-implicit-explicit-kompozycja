using System;

namespace Zadanie3
{
    class Program
    {
        static void Main(string[] args)
        {
            var xerox = new MultifunctionalDevice();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);

            xerox.ScanAndPrint();

            xerox.sendFax(doc1, "3333");
            xerox.ScanAndSend("4444");
            System.Console.WriteLine(xerox.FaxCounter);
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.PrintCounter);
            System.Console.WriteLine(xerox.ScanCounter);
            
        }
    }
}
