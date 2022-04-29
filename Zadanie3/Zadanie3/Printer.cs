using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
    class Printer : IPrinter
    {
		public int PrintCounter { get; set; } = 0;
		public new int Counter { get; set; } = 0;
	
		public IDevice.State state = IDevice.State.off;
		public IDevice.State GetState()
		{
			return state;
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
			state = IDevice.State.on;
			Counter++;
		}
		public void PowerOff()
		{
			state = IDevice.State.on;
		}
	}
}
