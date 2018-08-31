using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using MadMilkman.Ini;
using UnityEngine;

public abstract class SubConfig
{
    private static readonly Dictionary<Type, Func<string, object>> setter;
    private static readonly Dictionary<Type, Func<object, string>> getter;

    static SubConfig()
    {
        setter = new Dictionary<Type, Func<string, object>>();
        getter = new Dictionary<Type, Func<object, string>>();

        Register<Color>(
            s => {
                Color c;
                if(ColorUtility.TryParseHtmlString(s, out c))
                    return c;
                return Color.white;
            },
            c => "#" + ColorUtility.ToHtmlStringRGB(c));
        Register<Version>(
            s => Version.Parse(s),
            v => v?.ToString());
    }

    private static void Register<T>(Func<string, T> set, Func<T, string> get)
    {
        setter[typeof(T)] = s => set(s);
        getter[typeof(T)] = o => get((T)o);
    }

    private readonly Dictionary<string, PropertyInfo> props;

    public SubConfig()
    {
        props = new Dictionary<string, PropertyInfo>();
        foreach(var prop in GetType().GetProperties())
        {
            var attr = prop.GetCustomAttribute(typeof(IniSerializationAttribute)) as IniSerializationAttribute;
            if(attr != null && attr.Ignore && prop.Name.EndsWith("P"))
            {
                props[prop.Name.Substring(0, prop.Name.Length - 1)] = prop;
            }
        }
    }

    protected string Get([CallerMemberName]string name = null)
    {
        var type = props[name].PropertyType;
        return getter[type](props[name].GetValue(this));
    }

    protected void Set(string value, [CallerMemberName]string name = null)
    {
        var type = props[name].PropertyType;
        try
        {
            props[name].SetValue(this, setter[type](value));
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
}
