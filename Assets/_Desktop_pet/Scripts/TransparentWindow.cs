using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransparentWindow : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);
    
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cxLeftHeight;
        public int cxRightHeight;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    private const int GWL_EXSTYLE = -20;
    
    private const int WS_EX_LAYERED = 0x00080000;
    private const int WS_EX_TRANSPARENT = 0x00000020;

    private const uint LWA_COLORKEY = 0x00000001;
    
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    private IntPtr hWnd;

    private void Start()
    {
        // MessageBox(IntPtr.Zero, "Hello world!", "Hello Dialog", 0);
#if !UNITY_EDITOR
        
        hWnd = GetActiveWindow();

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        //SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);

        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
        Application.runInBackground = true;
#endif



    }

    private void Update()
    {
#if !UNITY_EDITOR
        SetClickThrough(!isMouseOverColliders() && !isMouseOverUI());
#endif        
    }

    private bool isMouseOverColliders()
    {
        return Physics2D.OverlapPoint(GetMouseWorldPosition());
    }

    private void SetClickThrough(bool clickThrough)
    {
        if (clickThrough)
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
        else
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
        }
    }
    
    public static Vector2 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        return new Vector2(worldPosition.x, worldPosition.y);
    }

    private bool isMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
