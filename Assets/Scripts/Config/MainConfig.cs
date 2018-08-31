using System;
using MadMilkman.Ini;
using UnityEngine;

public class MainConfig : SubConfig
{
    public MainConfig()
    {
        Autostart = true;
    }

    public bool Autostart { get; set; }

    public string Version { get { return Get(); } set { Set(value); } }

    [IniSerialization(true)]
    public Version VersionP { get; set; }
}
