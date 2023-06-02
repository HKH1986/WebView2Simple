using System;

namespace CheckBrowserWindowsApp.Web
{


    //
    // Summary:
    //     Defines values that specify the buttons on a mouse device.
    public enum MouseButton
    {
        //
        // Summary:
        //     The left mouse button.
        Left,
        //
        // Summary:
        //     The middle mouse button.
        Middle,
        //
        // Summary:
        //     The right mouse button.
        Right,
        //
        // Summary:
        //     The first extended mouse button.
        XButton1,
        //
        // Summary:
        //     The second extended mouse button.
        XButton2
    }


    public class MouseEvent : UIEvent
    {
        public bool isTrusted { get; set; }
        public int screenX { get; set; }
        public int screenY { get; set; }
        public int clientX { get; set; }
        public int clientY { get; set; }
        public bool ctrlKey { get; set; }
        public bool shiftKey { get; set; }
        public bool altKey { get; set; }
        public bool metaKey { get; set; }
        public int button { get; set; }
        public int buttons { get; set; }
        public int pageX { get; set; }
        public int pageY { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int offsetX { get; set; }
        public int offsetY { get; set; }
        public int movementX { get; set; }
        public int movementY { get; set; }
        public int layerX { get; set; }
        public int layerY { get; set; }
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


        public MouseButton MouseButton
        {
            get
            {
                return (MouseButton)Enum.ToObject(typeof(MouseButton), this.button);
            }
        }
    }
}
