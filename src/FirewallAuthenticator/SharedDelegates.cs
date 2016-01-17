using System;

/* 
/// <summary>
/// Borrowed from: http://www.codeproject.com/Articles/31163/Suppressing-Hosted-WebBrowser-Control-Dialogs
/// Licensed under the CPOL: http://www.codeproject.com/info/cpol10.aspx
/// </summary>
*/
namespace WebBrowserControlDialogs
{
    internal delegate T2 GenericDelegate<T1, T2>(T1 param);
    internal delegate T3 GenericDelegate<T1, T2, T3>(ref T1 param1, ref T2 param2);
}
