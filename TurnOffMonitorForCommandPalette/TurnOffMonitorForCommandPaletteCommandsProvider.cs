// Copyright (c) 2026 tanchekwei 
// Licensed under the MIT License. See the LICENSE file in the project root for details.
using System;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using TurnOffMonitorForCommandPalette.Classes;
using Windows.ApplicationModel;

namespace TurnOffMonitorForCommandPalette;

public partial class TurnOffMonitorForCommandPaletteCommandsProvider : CommandProvider
{
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
    private static extern IntPtr FindWindow(string? lpClassName, string? lpWindowName);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern IntPtr SendMessage(
        IntPtr hWnd,
        uint Msg,
        IntPtr wParam,
        IntPtr lParam);


    private readonly SettingsManager _settingsManager;

    public TurnOffMonitorForCommandPaletteCommandsProvider(SettingsManager settingsManager)
    {
        Id = Package.Current.Id.Name;
        _settingsManager = settingsManager;
        DisplayName = Constant.TurnOffMonitorDisplayName;
        Icon = Constant.IconExtension;
        Settings = _settingsManager.Settings;
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return [
            new CommandItem(
                new AnonymousCommand(() =>
                {
                    System.Threading.Thread.Sleep(_settingsManager.MonitorOffDelay);
                    var hwnd = FindWindow("Progman", "Program Manager");
                    if (hwnd != IntPtr.Zero)
                    {
                        SendMessage(hwnd, 0x0112, 0xF170, 2);
                    }
                })
                {
                    Icon = Constant.IconExtension,
                    Result = CommandResult.Hide(),
                    Name = Constant.TurnOffMonitorDisplayName,
                }
            )
            {
                MoreCommands = [new CommandContextItem(Settings!.SettingsPage)]
            }
        ];
    }
}
