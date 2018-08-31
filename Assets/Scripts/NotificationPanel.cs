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

    private PanelConfig cfg;

    private bool isDashboardOpen = false;
    private string format;
    private float opacity;
    private float dashOpacity;
    private float timer = 0f;

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

        if(timer > 0f && !isDashboardOpen)
        {
            timer -= Time.deltaTime;
            if(timer < 0f)
                timer = 0f;

            var op = Mathf.Lerp(cfg.Opacity, cfg.NotificationOpacity, timer / cfg.FadeTime);
            Overlay.SetOpacity(op);
        }
    }

    private void OnDashboardChange(bool open)
    {
        Overlay.SetOpacity(open ? dashOpacity : opacity);
        timer = cfg.FadeTime;
        isDashboardOpen = open;
    }

    public void SetConfig(PanelConfig cfg)
    {
        this.cfg = cfg;
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
        timer = cfg.FadeTime;
    }
}
