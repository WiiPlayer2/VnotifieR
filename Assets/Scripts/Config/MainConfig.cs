using System;

public class MainConfig : SubConfig
{
    public MainConfig()
    {
        Autostart = true;
    }

    public bool Autostart { get; set; }
}
