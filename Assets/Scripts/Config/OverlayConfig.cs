using System;
using MadMilkman.Ini;
using UnityEngine;

public class OverlayConfig : SubConfig
{
    public OverlayConfig()
    {
        PositionP = new Vector3(0, -0.3f, 1);
        RotationP = new Vector3(10, 0, 0);
    }

    public string Position { get { return Get(); } set { Set(value); } }

    [IniSerialization(true)]
    public Vector3 PositionP { get; set; }

    public string Rotation { get { return Get(); } set { Set(value); } }

    [IniSerialization(true)]
    public Vector3 RotationP { get; set; }
}
