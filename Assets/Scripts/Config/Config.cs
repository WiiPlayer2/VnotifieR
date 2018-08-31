//#define FORCE_CREATE
using System;
using System.ComponentModel;
using System.IO;
using MadMilkman.Ini;
using UnityEngine;

public class Config
{
    public static readonly Version CURRENT_VERSION = new Version(1, 0);

    public Config(string iniPath)
    {
        Ini = new IniFile();

#if !FORCE_CREATE
        if(File.Exists(iniPath))
            Ini.Load(iniPath);
#endif

        Main = Load<MainConfig>("Main");
        Server = Load<ServerConfig>("Server");
        Panel = Load<PanelConfig>("Panel");
        Overlay = Load<OverlayConfig>("Overlay");

        if(Main.VersionP != CURRENT_VERSION)
            Save(string.Format("{0}_{1}.ini", Path.GetFileNameWithoutExtension(iniPath), Main.VersionP));

        Main.VersionP = CURRENT_VERSION;
        Save(iniPath);
    }

    public IniFile Ini { get; private set; }

    public MainConfig Main { get; set; }
    public ServerConfig Server { get; set; }
    public PanelConfig Panel { get; set; }
    public OverlayConfig Overlay { get; set; }

    private T Load<T>(string section)
        where T : class, new()
    {
        IniSection s;
        if (Ini.Sections.Contains(section))
            s = Ini.Sections[section];
        else
            s = Ini.Sections.Add(section);
        return s.Deserialize<T>();
    }

    public void Save(string iniPath)
    {
        Refresh("Main", Main);
        Refresh("Server", Server);
        Refresh("Panel", Panel);
        Refresh("Overlay", Overlay);
        Ini.Save(iniPath);
    }

    private void Refresh<T>(string section, T subConfig)
        where T : class, new()
    {
        Ini.Sections.Remove(section);
        Ini.Sections.Add(section).Serialize(subConfig);
    }
}
