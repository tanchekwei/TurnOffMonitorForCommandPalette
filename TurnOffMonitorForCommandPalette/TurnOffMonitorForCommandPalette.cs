// Copyright (c) 2026 tanchekwei
// Licensed under the MIT License. See the LICENSE file in the project root for details.
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CommandPalette.Extensions;

namespace TurnOffMonitorForCommandPalette;

#if DEBUG
[Guid("6d3a1c47-f4f3-4908-abf9-7ce2bbf8ed16")]
#else
[Guid("8a81e06b-d66f-4403-b360-a0df4ce4d284")]
#endif
public sealed partial class TurnOffMonitorForCommandPalette : IExtension, IDisposable
{
    private readonly ManualResetEvent _extensionDisposedEvent;
    private readonly TurnOffMonitorForCommandPaletteCommandsProvider _provider;

    public TurnOffMonitorForCommandPalette(ManualResetEvent extensionDisposedEvent)
    {
        _extensionDisposedEvent = extensionDisposedEvent;
        var settingsManager = new SettingsManager();
        _provider = new TurnOffMonitorForCommandPaletteCommandsProvider(settingsManager);
    }

    public object? GetProvider(ProviderType providerType)
    {
        return providerType switch
        {
            ProviderType.Commands => _provider,
            _ => null,
        };
    }

    public void Dispose()
    {
        _extensionDisposedEvent.Set();
    }
}