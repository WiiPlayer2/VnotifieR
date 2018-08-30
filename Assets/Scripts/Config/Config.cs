using System;
using System.ComponentModel;
using System.IO;
using MadMilkman.Ini;
using UnityEngine;

public class Config
{
    public Config(string iniPath)
    {
        Ini = new IniFile();

#if !UNITY_EDITOR
        if(File.Exists(iniPath))
            Ini.Load(iniPath);
#endif

        Main = Load<MainConfig>("Main");
        Server = Load<ServerConfig>("Server");
        Panel = Load<PanelConfig>("Panel");

        Save(iniPath);
    }

    public IniFile Ini { get; private set; }

    public MainConfig Main { get; set; }
    public ServerConfig Server { get; set; }
    public PanelConfig Panel { get; set; }

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
        Ini.Save(iniPath);
    }

    private void Refresh<T>(string section, T subConfig)
        where T : class, new()
    {
        Ini.Sections.Remove(section);
        Ini.Sections.Add(section).Serialize(subConfig);
    }
}
