using System;
using MadMilkman.Ini;
using UnityEngine;

public class PanelConfig : SubConfig
{
    public PanelConfig()
    {
        ColorP = UnityEngine.Color.white;
        ShadowP = UnityEngine.Color.black;
        Opacity = 0.2f;
        DashboardOpacity = 1f;
        NotificationOpacity = 0.6f;
        FadeTime = 1f;
        FontSize = 30;
        Format = "{0}: {1}";
    }

    public string Color { get { return Get(); } set { Set(value); } }

    [IniSerialization(true)]
    public Color ColorP { get; set; }

    public string Shadow { get { return Get(); } set { Set(value); } }

    [IniSerialization(true)]
    public Color ShadowP { get; set; }

    public float Opacity { get; set; }

    public float NotificationOpacity { get; set; }

    public float DashboardOpacity { get; set; }

    public float FadeTime { get; set; }

    public int FontSize { get; set; }

    public string Format { get; set; }

}
