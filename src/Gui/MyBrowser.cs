using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace RefExplorer.Gui
{
  public partial class MyBrowser : WebBrowser
  {

    #region enums
    public enum OLECMDID
    {
      OLECMDID_OPEN = 1,
      OLECMDID_NEW = 2,
      OLECMDID_SAVE = 3,
      OLECMDID_SAVEAS = 4,
      OLECMDID_SAVECOPYAS = 5,
      OLECMDID_PRINT = 6,
      OLECMDID_PRINTPREVIEW = 7,
      OLECMDID_PAGESETUP = 8,
      OLECMDID_SPELL = 9,
      OLECMDID_PROPERTIES = 10,
      OLECMDID_CUT = 11,
      OLECMDID_COPY = 12,
      OLECMDID_PASTE = 13,
      OLECMDID_PASTESPECIAL = 14,
      OLECMDID_UNDO = 15,
      OLECMDID_REDO = 16,
      OLECMDID_SELECTALL = 17,
      OLECMDID_CLEARSELECTION = 18,
      OLECMDID_ZOOM = 19,
      OLECMDID_GETZOOMRANGE = 20,
      OLECMDID_UPDATECOMMANDS = 21,
      OLECMDID_REFRESH = 22,
      OLECMDID_STOP = 23,
      OLECMDID_HIDETOOLBARS = 24,
      OLECMDID_SETPROGRESSMAX = 25,
      OLECMDID_SETPROGRESSPOS = 26,
      OLECMDID_SETPROGRESSTEXT = 27,
      OLECMDID_SETTITLE = 28,
      OLECMDID_SETDOWNLOADSTATE = 29,
      OLECMDID_STOPDOWNLOAD = 30,
      OLECMDID_ONTOOLBARACTIVATED = 31,
      OLECMDID_FIND = 32,
      OLECMDID_DELETE = 33,
      OLECMDID_HTTPEQUIV = 34,
      OLECMDID_HTTPEQUIV_DONE = 35,
      OLECMDID_ENABLE_INTERACTION = 36,
      OLECMDID_ONUNLOAD = 37,
      OLECMDID_PROPERTYBAG2 = 38,
      OLECMDID_PREREFRESH = 39,
      OLECMDID_SHOWSCRIPTERROR = 40,
      OLECMDID_SHOWMESSAGE = 41,
      OLECMDID_SHOWFIND = 42,
      OLECMDID_SHOWPAGESETUP = 43,
      OLECMDID_SHOWPRINT = 44,
      OLECMDID_CLOSE = 45,
      OLECMDID_ALLOWUILESSSAVEAS = 46,
      OLECMDID_DONTDOWNLOADCSS = 47,
      OLECMDID_UPDATEPAGESTATUS = 48,
      OLECMDID_PRINT2 = 49,
      OLECMDID_PRINTPREVIEW2 = 50,
      OLECMDID_SETPRINTTEMPLATE = 51,
      OLECMDID_GETPRINTTEMPLATE = 52,
      OLECMDID_PAGEACTIONBLOCKED = 55,
      OLECMDID_PAGEACTIONUIQUERY = 56,
      OLECMDID_FOCUSVIEWCONTROLS = 57,
      OLECMDID_FOCUSVIEWCONTROLSQUERY = 58,
      OLECMDID_SHOWPAGEACTIONMENU = 59,
      OLECMDID_ADDTRAVELENTRY = 60,
      OLECMDID_UPDATETRAVELENTRY = 61,
      OLECMDID_UPDATEBACKFORWARDSTATE = 62,
      OLECMDID_OPTICAL_ZOOM = 63,
      OLECMDID_OPTICAL_GETZOOMRANGE = 64,
      OLECMDID_WINDOWSTATECHANGED = 65

    }

    public enum OLECMDEXECOPT
    {
      OLECMDEXECOPT_DODEFAULT,
      OLECMDEXECOPT_PROMPTUSER,
      OLECMDEXECOPT_DONTPROMPTUSER,
      OLECMDEXECOPT_SHOWHELP
    }

    public enum OLECMDF
    {
      OLECMDF_DEFHIDEONCTXTMENU = 0x20,
      OLECMDF_ENABLED = 2,
      OLECMDF_INVISIBLE = 0x10,
      OLECMDF_LATCHED = 4,
      OLECMDF_NINCHED = 8,
      OLECMDF_SUPPORTED = 1
    }
    #endregion

    #region IWebBrowser2
    [ComImport, /*SuppressUnmanagedCodeSecurity,*/ TypeLibType(TypeLibTypeFlags.FOleAutomation | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FHidden), Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E")]
    public interface IWebBrowser2
    {
      [DispId(100)]
      void GoBack();
      [DispId(0x65)]
      void GoForward();
      [DispId(0x66)]
      void GoHome();
      [DispId(0x67)]
      void GoSearch();
      [DispId(0x68)]
      void Navigate([In] string Url, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers);
      [DispId(-550)]
      void Refresh();
      [DispId(0x69)]
      void Refresh2([In] ref object level);
      [DispId(0x6a)]
      void Stop();
      [DispId(200)]
      object Application { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
      [DispId(0xc9)]
      object Parent { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
      [DispId(0xca)]
      object Container { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
      [DispId(0xcb)]
      object Document { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
      [DispId(0xcc)]
      bool TopLevelContainer { get; }
      [DispId(0xcd)]
      string Type { get; }
      [DispId(0xce)]
      int Left { get; set; }
      [DispId(0xcf)]
      int Top { get; set; }
      [DispId(0xd0)]
      int Width { get; set; }
      [DispId(0xd1)]
      int Height { get; set; }
      [DispId(210)]
      string LocationName { get; }
      [DispId(0xd3)]
      string LocationURL { get; }
      [DispId(0xd4)]
      bool Busy { get; }
      [DispId(300)]
      void Quit();
      [DispId(0x12d)]
      void ClientToWindow(out int pcx, out int pcy);
      [DispId(0x12e)]
      void PutProperty([In] string property, [In] object vtValue);
      [DispId(0x12f)]
      object GetProperty([In] string property);
      [DispId(0)]
      string Name { get; }
      [DispId(-515)]
      int HWND { get; }
      [DispId(400)]
      string FullName { get; }
      [DispId(0x191)]
      string Path { get; }
      [DispId(0x192)]
      bool Visible { get; set; }
      [DispId(0x193)]
      bool StatusBar { get; set; }
      [DispId(0x194)]
      string StatusText { get; set; }
      [DispId(0x195)]
      int ToolBar { get; set; }
      [DispId(0x196)]
      bool MenuBar { get; set; }
      [DispId(0x197)]
      bool FullScreen { get; set; }
      [DispId(500)]
      void Navigate2([In] ref object URL, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers);
      [DispId(0x1f5)]
      OLECMDF QueryStatusWB([In] OLECMDID cmdID);
      [DispId(0x1f6)]
      void ExecWB([In] OLECMDID cmdID, [In] OLECMDEXECOPT cmdexecopt, ref object pvaIn, IntPtr pvaOut);
      [DispId(0x1f7)]
      void ShowBrowserBar([In] ref object pvaClsid, [In] ref object pvarShow, [In] ref object pvarSize);
      [DispId(-525)]
      WebBrowserReadyState ReadyState { get; }
      [DispId(550)]
      bool Offline { get; set; }
      [DispId(0x227)]
      bool Silent { get; set; }
      [DispId(0x228)]
      bool RegisterAsBrowser { get; set; }
      [DispId(0x229)]
      bool RegisterAsDropTarget { get; set; }
      [DispId(0x22a)]
      bool TheaterMode { get; set; }
      [DispId(0x22b)]
      bool AddressBar { get; set; }
      [DispId(0x22c)]
      bool Resizable { get; set; }
    }
    #endregion

    private IWebBrowser2 axIWebBrowser2;

    public MyBrowser()
    {           
    }

    protected override void AttachInterfaces(object nativeActiveXObject)
    {
      base.AttachInterfaces(nativeActiveXObject);
      this.axIWebBrowser2 = (IWebBrowser2)nativeActiveXObject;
    }

    protected override void DetachInterfaces()
    {
      base.DetachInterfaces();
      this.axIWebBrowser2 = null;
    }

    public void Zoom(int factor)
    {

      object pvaIn = factor;
      try
      {
        this.axIWebBrowser2.ExecWB(OLECMDID.OLECMDID_OPTICAL_ZOOM, OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, ref pvaIn, IntPtr.Zero);
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}