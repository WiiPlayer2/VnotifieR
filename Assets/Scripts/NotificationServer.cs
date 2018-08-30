using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Net;
using MadMilkman.Ini;

public class NotificationServer : MonoBehaviour
{
    public TextAsset DefaultIniContent;
    public string IniPath;
    public NotificationPanel NotificationPanel;

    private Config config;
    private HttpListener httpListener;
    private IAsyncResult currentGetContext;

    void Start()
    {
        config = new Config(IniPath);

        NotificationPanel.SetConfig(config.Panel);
        InitServer();
    }

    private void InitServer()
    {
        httpListener = new HttpListener();
        var port = config.Server.Port;
        foreach(var bind in config.Server.Binds)
        {
            httpListener.Prefixes.Add(string.Format("http://{0}:{1}/", bind.Trim(), port));
        }

        httpListener.Start();
        currentGetContext = httpListener.BeginGetContext(null, null);
    }

    void Update()
    {
        if(currentGetContext != null && currentGetContext.IsCompleted)
        {
            var context = httpListener.EndGetContext(currentGetContext);
            currentGetContext = httpListener.BeginGetContext(null, null);
            HandleContext(context);
        }
    }

    private void HandleContext(HttpListenerContext context)
    {
        var failed = true;
        try
        {
            if(context.Request.Url.AbsolutePath != "/notification")
                return;
            if(context.Request.HttpMethod != "POST")
                return;
            
            using(var reader = new StreamReader(context.Request.InputStream))
            {
                var body = reader.ReadToEnd();
                var json = new JSONObject(body);
                var not = Notification.FromJson(json);
                NotificationPanel.Show(not);
            }

            failed = false;
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }
        finally
        {
            if(failed)
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Response.Close();
        }
    }
}
