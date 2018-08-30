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

    public void SetConfig(PanelConfig cfg)
    {
        TextPanel.color        = cfg.ColorP;
        TextShadow.effectColor = cfg.ShadowP;
        TextPanel.fontSize     = cfg.FontSize;
        format                 = cfg.Format;
        opacity                = cfg.Opacity;
        dashOpacity            = cfg.DashboardOpacity;
    }

    public void Show(Notification notification)
    {
        TextPanel.text += "\n" + string.Format(format, notification.Title, notification.Content);
        if(TextPanel.text.Length > MAX_TEXT_LENGTH)
            TextPanel.text = TextPanel.text.Substring(TextPanel.text.Length - MAX_TEXT_LENGTH);
    }
}
