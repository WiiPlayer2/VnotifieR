using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification
{
    public string Title { get; set; }
    public string Content { get; set; }

    public static Notification FromJson(JSONObject json)
    {
        return new Notification
        {
            Title = json.GetString("title"),
            Content = json.GetString("content"),
        };
    }
}