

using Common.Interop;
using System;
using System.Windows.Input;

namespace Common
{
    public class KeyHandler
    {
    
        public event EventHandler<int> KeyPressed;
        public event EventHandler<int> KeyReleased;

        public event EventHandler ShiftPressed;
        public event EventHandler CtrlPressed;
        public event EventHandler AltPressed;

        public int WindowHandle { get; set; }


        private bool IsWindowActive()
        {
            return (int)Winapi.GetForegroundWindow() == WindowHandle;
        }

        #region handler methods
        public bool Handle(int vkCode, bool down = true)
        {
            //if (!IsWindowActive())
            //    return true;

            if (down)
                KeyPressed.Invoke(this, vkCode);
            else
                KeyReleased.Invoke(this, vkCode);

            return true;
        }



        #endregion
    }
}
