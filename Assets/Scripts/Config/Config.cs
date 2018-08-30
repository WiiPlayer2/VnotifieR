using System;
using System.ComponentModel;
using System.IO;
using MadMilkman.Ini;
using UnityEngine;

public class Config
{
    public Config(string iniPath, string defaultContent)
    {
#if !UNITY_EDITOR
        if(!File.Exists(iniPath))
        {
            File.WriteAllText(iniPath, defaultContent);
        }
#else
        //File.WriteAllText(iniPath, defaultContent);
#endif

        Ini = new IniFile();
        Ini.Load(iniPath);

        Server = Ini.Sections["Server"].Deserialize<ServerConfig>();
        Panel = Ini.Sections["Panel"].Deserialize<PanelConfig>();

        Save(iniPath);
    }

    public IniFile Ini { get; private set; }

    public ServerConfig Server { get; set; }
    public PanelConfig Panel { get; set; }

    public void Save(string iniPath)
    {
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
