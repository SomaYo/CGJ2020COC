using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class WindowAlpha : MonoBehaviour
{
    #region Win函数常量
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, int bAlpha, int dwFlags);

    [DllImport("Dwmapi.dll")]
    static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    private const int WS_POPUP = 0x800000;
    private const int GWL_EXSTYLE = -20;
    private const int GWL_STYLE = -16;
    private const int WS_EX_LAYERED = 0x00080000;
    private const int WS_BORDER = 0x00800000;
    private const int WS_CAPTION = 0x00C00000;
    private const int SWP_SHOWWINDOW = 0x0040;
    private const int LWA_COLORKEY = 0x00000001;
    private const int LWA_ALPHA = 0x00000002;
    private const int WS_EX_TRANSPARENT = 0x20;
    //
    private const int ULW_COLORKEY = 0x00000001;
    private const int ULW_ALPHA = 0x00000002;
    private const int ULW_OPAQUE = 0x00000004;
    private const int ULW_EX_NORESIZE = 0x00000008;
    #endregion

    public int windowWidth;//窗口宽度
    public int windowHeight;//窗口高度

    private IntPtr hwnd;
    private int posX;
    private int posY;

    void Awake()
    {
#if UNITY_EDITOR
        print("编辑模式无法使用！");
#else
        hwnd = GetActiveWindow();
        posX = Screen.currentResolution.width / 2 - windowWidth / 2;
        posY = Screen.currentResolution.height / 2 - windowHeight / 2;
        SetWindowPos(hwnd, -1, posX, posY, windowWidth, windowHeight, SWP_SHOWWINDOW);

        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_BORDER & ~WS_CAPTION);SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_BORDER & ~WS_CAPTION);
     
        var margins = new MARGINS() { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hwnd, ref margins);
#endif
    }

    void OnApplicationQuit()
    {
        //程序退出的时候设置窗体为0像素，从打开到走到awake也需要一定是时间
        //会先有窗体边框，然后透明，这样会有闪一下的效果，
        //设置窗体为0像素后，下次打开是就是0像素，走到awake再设置回来正常的窗口大小
        //便能解决程序加载时会闪白色边框的现象
        SetWindowPos(hwnd, -1, posX, posY, 0, 0, SWP_SHOWWINDOW);
    }
}