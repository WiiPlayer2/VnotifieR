using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour
{
    private const int MAX_TEXT_LENGTH = 0x8000;

    public Unity_Overlay Overlay;
    public Text TextPanel;
    public Shadow TextShadow;

    private string format;
    private float opacity;
    private float dashOpacity;

    void Start()
    {
        TextPanel.text = "=== VnotifieR ===";
        OnDashboardChange(false);
    }

    private void Update()
    {
        if(Overlay.overlay != null)
        {
            Overlay.overlay.onDashboardChange = OnDashboardChange;
        }
    }

    private void OnDashboardChange(bool open)
    {
        Overlay.SetOpacity(open ? dashOpacity : opacity);
    }

    public void SetConfig(Ini ini)
    {
        TextPanel.color        = Helper.ParseColor(ini.GetValue("color", "Panel", "ffffff"), Color.white);
        TextShadow.effectColor = Helper.ParseColor(ini.GetValue("shadow", "Panel", "000000"), Color.black);
        TextPanel.fontSize     = Helper.ParseInt(ini.GetValue("font_size", "Panel", "30"), 30);
        format                 = ini.GetValue("format", "Panel", "{0}: {1}");
        opacity                = Helper.ParseFloat(ini.GetValue("opacity", "Panel", "0.2"), 0.2f);
        dashOpacity            = Helper.ParseFloat(ini.GetValue("dashboard_opacity", "Panel", "1"), 1f);
    }

    public void Show(Notification notification)
    {
        TextPanel.text += "\n" + string.Format(format, notification.Title, notification.Content);
        if(TextPanel.text.Length > MAX_TEXT_LENGTH)
            TextPanel.text = TextPanel.text.Substring(TextPanel.text.Length - MAX_TEXT_LENGTH);
    }
}
