using System;
using MadMilkman.Ini;
using UnityEngine;

public class PanelConfig : SubConfig
{
    public string Color { get { return Get(); } set { Set(value); } }

    [IniSerialization(true)]
    public Color ColorP { get; set; }

    public string Shadow { get { return Get(); } set { Set(value); } }

    [IniSerialization(true)]
    public Color ShadowP { get; set; }

    public float Opacity { get; set; }

    public float DashboardOpacity { get; set; }

    public int FontSize { get; set; }

    public string Format { get; set; }
}
