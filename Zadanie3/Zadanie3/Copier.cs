using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
    public class Copier : BaseDevice
    {
		public int PrintCounter { get; set; }
		public int ScanCounter { get; set; }

		public new int Counter { get; set; }

		public Printer Printer { get; set; }
		public Scanner Scanner { get; set; }

		public Copier()
		{
			Printer = new Printer();
			Scanner = new Scanner();
		}
		public void Print(in IDocument document)
		{
			if (state == IDevice.State.on)
			{
				Printer.PowerOn();
				Printer.Print(document);
				Printer.PowerOff();
			}
		}

		public void PowerOn()
		{
			if (GetState() == IDevice.State.off)
				Counter++;

			base.PowerOn();
		}


		public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
		{
			if (state == IDevice.State.on)
			{
				Scanner.PowerOn();
				Scanner.Scan(out document, formatType);
				Scanner.PowerOff();
			}
			else document = null;

		}

		public void ScanAndPrint()
		{
			Scan(out IDocument document, IDocument.FormatType.JPG);
			Print(document);
		}
	}
}
