using System;
using System.Globalization;
using UnityEngine;

public static class Helper
{
    public static Color ParseColor(string hex, Color fallback)
    {
        if(hex.Length != 6)
            return fallback;
        
        var vals = new float[3];
        for(var i = 0; i < 3; i++)
        {
            var subHex = hex.Substring(i * 2, 2);
            byte val;
            if(byte.TryParse(subHex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out val))
                vals[i] = val / 255f;
            else
                return fallback;
        }

        return new Color(vals[0], vals[1], vals[2]);
    }

    public static float ParseFloat(string str, float fallback)
    {
        var ret = 0f;
        if(float.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out ret))
            return ret;
        return fallback;
    }

    public static int ParseInt(string str, int fallback)
    {
        var ret = 0;
        if(int.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out ret))
            return ret;
        return fallback;
    }

    public static string GetString(this JSONObject json, string field)
    {
        var str = "";
        json.GetField(out str, field, null);
        return str;
    }
}