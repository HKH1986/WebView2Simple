
namespace CheckBrowserWindowsApp.Web
{
    public class KeyboardEvent : UIEvent
    {

        public bool isTrusted { get; set; }
        public string key { get; set; }
        public string code { get; set; }
        public int location { get; set; }
        public bool ctrlKey { get; set; }
        public bool shiftKey { get; set; }
        public bool altKey { get; set; }
        public bool metaKey { get; set; }
        public bool repeat { get; set; }
        public bool isComposing { get; set; }
        public int charCode { get; set; }
        public int keyCode { get; set; }
        public int DOM_KEY_LOCATION_STANDARD { get; set; }
        public int DOM_KEY_LOCATION_LEFT { get; set; }
        public int DOM_KEY_LOCATION_RIGHT { get; set; }
        public int DOM_KEY_LOCATION_NUMPAD { get; set; }
        public int detail { get; set; }
        public int which { get; set; }
        public string type { get; set; }
        public int eventPhase { get; set; }
        public bool bubbles { get; set; }
        public bool cancelable { get; set; }
        public bool defaultPrevented { get; set; }
        public bool composed { get; set; }
        public bool returnValue { get; set; }
        public bool cancelBubble { get; set; }
        public int NONE { get; set; }
        public int CAPTURING_PHASE { get; set; }
        public int AT_TARGET { get; set; }
        public int BUBBLING_PHASE { get; set; }


    }
}
