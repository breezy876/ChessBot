using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Input;
    using Common;

    public class KeyboardHook
    {
        #region pinvoke details

        #region Windows constants

        /// Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard  input events.
        /// </summary>
        private const int WH_KEYBOARD_LL = 13;

        /// <summary>
        /// Installs a hook procedure that monitors mouse messages. For more information, see the MouseProc hook procedure. 
        /// </summary>
        private const int WH_MOUSE = 7;

        /// <summary>
        /// Installs a hook procedure that monitors keystroke messages. For more information, see the KeyboardProc hook procedure. 
        /// </summary>
        private const int WH_KEYBOARD = 2;

        /// <summary>
        /// The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem 
        /// key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed.
        /// </summary>
        private const int WM_KEYDOWN = 0x100;

        /// <summary>
        /// The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem 
        /// key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed, 
        /// or a keyboard key that is pressed when a window has the keyboard focus.
        /// </summary>
        private const int WM_KEYUP = 0x101;

        /// <summary>
        /// The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user 
        /// presses the F10 key (which activates the menu bar) or holds down the ALT key and then 
        /// presses another key. It also occurs when no window currently has the keyboard focus; 
        /// in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that 
        /// receives the message can distinguish between these two contexts by checking the context 
        /// code in the lParam parameter. 
        /// </summary>
        private const int WM_SYSKEYDOWN = 0x104;

        /// <summary>
        /// The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user 
        /// releases a key that was pressed while the ALT key was held down. It also occurs when no 
        /// window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent 
        /// to the active window. The window that receives the message can distinguish between 
        /// these two contexts by checking the context code in the lParam parameter. 
        /// </summary>
        private const int WM_SYSKEYUP = 0x105;
        #endregion

        private enum HookType : int
        {
            WH_KEYBOARD = 2,
            WH_KEYBOARD_LL = 13,
        }

        private struct keyboardHookStruct
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr extraInfo;
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(
            HookType code, HookProc func, IntPtr instance, int threadID);

        [DllImport("user32.dll")]
        private static extern int UnhookWindowsHookEx(IntPtr hook);

        [DllImport("user32.dll")]
        private static extern int CallNextHookEx(
            IntPtr hook, int code, IntPtr wParam, ref keyboardHookStruct lParam);

        #endregion


        HookType hookType = HookType.WH_KEYBOARD_LL;
        IntPtr hookHandle = IntPtr.Zero;
        HookProc hookProc = null;

        private KeyHandler keyHandler;

        public KeyboardHook(KeyHandler handler)
        {
            keyHandler = handler;
        }

        // hook method called by system
        private delegate int HookProc(int code, IntPtr wParam, ref keyboardHookStruct lParam);

        public bool Installed { get; private set; }

        // hook function called by system
        private int HookCallback(int code, IntPtr wParam, ref keyboardHookStruct lParam)
        {

            if (code < 0)
                return CallNextHookEx(hookHandle, code, wParam, ref lParam);

            ////if (!HSC.AgentPerformance.InPerformanceMode)
            ////    return CallNextHookEx(hookHandle, code, wParam, ref lParam);

            if (wParam.ToInt32() == WM_KEYDOWN && (int)lParam.extraInfo == 0)
            {

                Debug.WriteLine($"Pressed key: {lParam.vkCode}");

                //if (lParam.extraInfo == new IntPtr(0x8000))
                //    return CallNextHookEx(hookHandle, code, wParam, ref lParam);
                bool result = keyHandler.Handle((int)lParam.vkCode, wParam.ToInt32() == WM_KEYDOWN);

                if (!result)
                    return 1;

                return CallNextHookEx(hookHandle, code, wParam, ref lParam);

            }

            else if (wParam.ToInt32() == WM_KEYUP && (int)lParam.extraInfo == 0)
            {

                Debug.WriteLine($"Released key: {lParam.vkCode}");

                //if (lParam.extraInfo == new IntPtr(0x8000))
                //    return CallNextHookEx(hookHandle, code, wParam, ref lParam);


                bool result = keyHandler.Handle((int)lParam.vkCode, wParam.ToInt32() == WM_KEYDOWN);

                if (!result)
                    return 1;

                return CallNextHookEx(hookHandle, code, wParam, ref lParam);
            }

            return CallNextHookEx(hookHandle, code, wParam, ref lParam);
        }

        public void Install()
        {
            hookProc = new HookProc(HookCallback);

            // need ssinstance handle to module to create a system-wide hook
            Module[] list = Assembly.GetExecutingAssembly().GetModules();
            Debug.Assert(list != null && list.Length > 0);

            // install system-wide hook
            hookHandle = SetWindowsHookEx(hookType,
                hookProc, IntPtr.Zero, 0);

            if (hookHandle != IntPtr.Zero)
                Installed = true;
        }

        public void Uninstall()
        {
            if (hookHandle != IntPtr.Zero)
            {
                // uninstall system-wide hook
                UnhookWindowsHookEx(hookHandle);
                hookHandle = IntPtr.Zero;
                Installed = false;
            }
        }
    }
}
