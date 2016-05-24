using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BernieApp.UWP.Messages
{
    class AlertMessage : MessageBase
    {
        public string Id { get; set; }
        public string Path { get; set; }
    }
}
