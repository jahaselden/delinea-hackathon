using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace TestMCP;

[McpServerToolType]
public sealed class GitHubTool
{
    GitHubService gitHubService;

    public GitHubTool(GitHubService gitHubService)
    {
        this.gitHubService = gitHubService;
    }

    [McpServerTool, Description("Get all information about a GitHub user by username.")]
    public static async Task<string> GetUserInfo(GitHubService gitHubService,
        [Description("The username associated with a github account")] string username) 
    {
        var userInfo = await gitHubService.GetUserInfoAsync(username);
        return JsonSerializer.Serialize(userInfo);
    }

    [McpServerTool, Description("Get information about a GitHub user's location by username.")]
    public static async Task<string> GetUserLocation(GitHubService gitHubService,
            [Description("The username associated with a github account")] string username) {
        var userInfo = await gitHubService.GetUserLocation(username);
        return JsonSerializer.Serialize(userInfo);
    }

}