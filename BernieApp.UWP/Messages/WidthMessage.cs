using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace BernieApp.UWP.Messages
{
    public class WidthMessage : MessageBase
    {
        public double Width { get; set; }
    }
}
