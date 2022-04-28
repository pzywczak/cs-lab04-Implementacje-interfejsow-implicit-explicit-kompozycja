using System;

namespace ver1
{
	public class Copier : BaseDevice, IPrinter, IScanner
	{
		public int PrintCounter { get; set; } = 0;
		public int ScanCounter { get; set; } = 0;
		public int Counter { get; set; }

		public void Print(in IDocument document)
		{
			if (state == IDevice.State.on)
				Console.WriteLine("{0} Print: {1}", DateTime.Today, document.GetFileName());
		}
	}

}
