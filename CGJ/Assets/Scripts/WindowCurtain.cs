using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class WindowCurtain : MonoBehaviour
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
    private const int GWL_EXSTYLE = -20;
    private const int GWL_STYLE = -16;
    private const int WS_EX_LAYERED = 0x00080000;
    private const int WS_BORDER = 0x00800000;
    private const int WS_CAPTION = 0x00C00000;
    private const int SWP_SHOWWINDOW = 0x0040;
    #endregion

    private static WindowCurtain _Instance;
    public static WindowCurtain Get()
    {
        return _Instance;
    }

    public level4camera flagobject;

    private Image _image;

    public float MaskSize
    {
        get
        {
            var material = _image.material;
            if (material != null)
            {
                return material.GetFloat("_MaskSize");
            }
            return 1.0F;
        }

        set
        {
            var material = _image.material;
            if (material != null)
            {
                material.SetFloat("_MaskSize", value);
            }
        }
    }

    private void OnEnable()
    {
        _Instance = this;
        _image = GetComponent<Image>();

    }
#if !UNITY_EDITOR
    private void Awake()
    {
        var hwnd = GetActiveWindow();
        //去边框并且透明
        SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED);
        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_BORDER & ~WS_CAPTION);
        // 设置Margins
        var margins = new MARGINS() { cxLeftWidth = -1 };
        // 扩展Aero Glass
        margins.cxLeftWidth = -1;
        DwmExtendFrameIntoClientArea(hwnd, ref margins);
}
#endif
    private void Update()
    {
        if (flagobject.start&&!flagobject.pause)
        {
            if (MaskSize > 0)
                MaskSize -= 0.001F;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (MaskSize < 1)
                MaskSize += 0.01F;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (MaskSize > 0)
                MaskSize -= 0.01F;
        }
        if (flagobject.finish)
        {
            MaskSize  -=0.1F;
        }
    }
}