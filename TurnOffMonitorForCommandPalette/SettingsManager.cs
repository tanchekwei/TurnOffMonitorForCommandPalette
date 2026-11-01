// Copyright (c) 2026 tanchekwei 
// Licensed under the MIT License. See the LICENSE file in the project root for details.
using System.IO;
using Microsoft.CommandPalette.Extensions.Toolkit;
using TurnOffMonitorForCommandPalette.Classes;

namespace TurnOffMonitorForCommandPalette;

public class SettingsManager : JsonSettingsManager
{
    private static readonly string _namespace = "turnOffMonitor";

    private static string Namespaced(string propertyName) => $"{_namespace}.{propertyName}";

    public static SettingsManager? Instance { get; private set; }

    private readonly TextSetting _monitorOffDelay = new(
        Namespaced(nameof(MonitorOffDelay)),
        "Monitor Off Delay (ms)",
        "Wait for keyboard input to finish before turning off the monitor.\nIncrease if the display wakes up immediately;\nDecrease if you want it to turn off faster.\nSet in milliseconds (ms).",
        "500");

    public int MonitorOffDelay
    {
        get
        {
            if (int.TryParse(_monitorOffDelay.Value, out int size) && size >= 0)
            {
                return size;
            }
            return 500;
        }
    }

    public static string GetJsonPath(string fileName)
    {
        var directory = Constant.SettingsFolderPath;
        Directory.CreateDirectory(directory);
        return Path.Combine(directory, fileName);
    }

    internal static string SettingsJsonPath() => GetJsonPath("settings.json");

    public SettingsManager()
    {
        Instance = this;
        FilePath = SettingsJsonPath();
        Settings.Add(_monitorOffDelay);

        LoadSettings();

        Settings.SettingsChanged += (s, a) =>
        {
            SaveSettings();
        };
    }
}