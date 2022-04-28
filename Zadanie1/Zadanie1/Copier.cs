using System;

namespace ver1
{
	public class Copier : BaseDevice, IPrinter, IScanner
	{
		public int PrintCounter { get; set; } = 0;
		public int ScanCounter { get; set; } = 0;
		public int Counter { get; set; }


	}

}
