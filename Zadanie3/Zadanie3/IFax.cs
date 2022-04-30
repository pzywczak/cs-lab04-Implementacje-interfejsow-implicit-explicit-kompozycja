using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
    public interface IFax : IDevice
    {
        public void sendFax(IDocument document, string faxNumber);
    }
}
