// Copyright (c) 2026 tanchekwei 
// Licensed under the MIT License. See the LICENSE file in the project root for details.
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace TurnOffMonitorForCommandPalette.Classes;

public static class Constant
{
#if DEBUG
    public const string AppName = "TurnOffMonitorForCommandPaletteDev";
    public const string TurnOffMonitorDisplayName = "Turn Off Monitor (Dev)";
#else
    public const string AppName = "TurnOffMonitorForCommandPalette";
    public const string TurnOffMonitorDisplayName = "Turn Off Monitor";
#endif
    public static readonly string SettingsFolderPath = Utilities.BaseSettingsPath(AppName);
    public static readonly IconInfo IconExtension = IconHelpers.FromRelativePath(@"Assets\icon.svg");
}