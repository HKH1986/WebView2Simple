
using System;

namespace CheckBrowserWindowsApp.Web
{
    public class UIEvent : EventArgs
    {

        public double timeStamp { get; set; }



        public string eventRealName { get; set; }
        public string eventName { get; set; }
        public string serverEventName { get; set; }
        public string elementID { get; set; }
        public string serverUniqueID { get; set; }

        public string elementName { get; set; }
        public string targetOuterHTML { get; set; }


    }
}
