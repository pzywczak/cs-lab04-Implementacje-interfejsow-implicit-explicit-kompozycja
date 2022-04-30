using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
    public class Fax : IFax
    {
        public int Counter { get; private set; } = 0;
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff()
        {
            state = IDevice.State.off;
        }

        public void PowerOn()
        {
            state = IDevice.State.on;
        }
        public void sendFax(IDocument document, string faxNumber)
        {
            if (state == IDevice.State.on)
            {
                Counter++;
                Console.WriteLine($"{DateTime.Today} Sent: {document.GetFileName()} from: {this.FaxNumber} to: {faxNumber}");
            }
        }
    }
}
