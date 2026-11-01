// Copyright (c) Jonah Fintz
using System;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CommandPalette.Extensions;
using TurnOffMonitorForCommandPalette.Classes;

[assembly: NeutralResourcesLanguage("en-US")]

namespace TurnOffMonitorForCommandPalette;

public class Program
{
    public static void Show(string title, string message)
    {
        _ = MessageBoxW(IntPtr.Zero, message, title, 0);
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBoxW(
        IntPtr hWnd,
        string text,
        string caption,
        uint type);

    [MTAThread]
    public static void Main(string[] args)
    {
            if (args.Length > 0 && args[0] == "-RegisterProcessAsComServer")
            {
                using ExtensionServer server = new();
                var extensionDisposedEvent = new ManualResetEvent(false);

                // We are instantiating an extension instance once above, and returning it every time the callback in RegisterExtension below is called.
                // This makes sure that only one instance of SampleExtension is alive, which is returned every time the host asks for the IExtension object.
                // If you want to instantiate a new instance each time the host asks, create the new instance inside the delegate.
                var extensionInstance = new TurnOffMonitorForCommandPalette(extensionDisposedEvent);
                server.RegisterExtension(() => extensionInstance);
                // This will make the main thread wait until the event is signalled by the extension class.
                // Since we have single instance of the extension object, we exit as soon as it is disposed.
                extensionDisposedEvent.WaitOne();
            }
            else
            {
                string productName = Constant.TurnOffMonitorDisplayName;
                Show(
                    productName,
                    $"{productName} is a PowerToys Command Palette extension.\n\nInstall and enable PowerToys Command Palette,\n\nthen open {productName} extension from Command Palette.");
            }
    }
}
