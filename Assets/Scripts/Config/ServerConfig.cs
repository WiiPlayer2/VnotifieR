using System;
using MadMilkman.Ini;

public class ServerConfig
{
    public ServerConfig()
    {
        Binds = new string[] { "localhost", "127.0.0.1" };
        Port = 45689;
    }

    public string[] Binds { get; set; }

    public int Port { get; set; }
}
