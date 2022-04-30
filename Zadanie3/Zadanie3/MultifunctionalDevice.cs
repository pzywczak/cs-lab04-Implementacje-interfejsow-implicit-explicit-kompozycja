using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
	public class MultifunctionalDevice : Copier
	{

		public int PrintCounter { get; set; } = 0;
		public int ScanCounter { get; set; } = 0;
		public new int Counter { get; set; }
		public string FaxNumber { get; set; }
		public int FaxCounter { get; set; } = 0;
		public Fax Fax { get; private set; }


		public MultifunctionalDevice()
		{
			Fax = new Fax();
		}
		public void Print(in IDocument document)
		{
			if (GetState() == IDevice.State.off)
				return;

			if (state == IDevice.State.on)
			{
				Console.WriteLine("{0} Print: {1}", DateTime.Now, document.GetFileName());
				PrintCounter++;
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
			string type = "";


			switch (formatType)
			{
				case IDocument.FormatType.TXT:
					type = "Text";
					break;
				case IDocument.FormatType.PDF:
					type = "PDF";
					break;
				case IDocument.FormatType.JPG:
					type = "Image";
					break;
				default:
					throw new Exception("Invalid type!");

			}

			string nameDocument = string.Format($"{type}{ScanCounter + 1}.{formatType.ToString().ToLower()}");

			if (formatType == IDocument.FormatType.PDF)
			{
				document = new PDFDocument(nameDocument);
			}
			else if (formatType == IDocument.FormatType.TXT)
			{
				document = new TextDocument(nameDocument);
			}
			else
			{
				document = new ImageDocument(nameDocument);
			}

			if (state == IDevice.State.on)
			{
				ScanCounter++;
				Console.WriteLine("{0}, Scan: {1}", DateTime.Now, document.GetFileName());
			}

		}
		public void ScanAndPrint()
		{
			Scan(out IDocument document, IDocument.FormatType.JPG);
			Print(document);
		}

		public void sendFax(IDocument document, string faxNumber)
		{
			if (state == IDevice.State.on)
			{
				Fax.PowerOn();
				Fax.sendFax(document, faxNumber);
				Fax.PowerOff();
			}
		}

		public void ScanAndSend(string faxNumber)
		{
			Scan(out IDocument doc);
			sendFax(doc, faxNumber);
		}

	}
}

