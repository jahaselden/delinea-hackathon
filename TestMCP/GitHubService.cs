using Octokit;

namespace TestMCP;

public class GitHubService
{
    private readonly GitHubClient gitHubClient;
    public GitHubService()
    {
        this.gitHubClient = new GitHubClient(new ProductHeaderValue("TestMCP"));
    }

    /// <summary>
    /// Gets a GitHub user's bio by username
    /// </summary>
    /// <param name="username">The GitHub username to look up</param>
    /// <returns>The user's bio, or null if not available</returns>
    public async Task<string?> GetUserLocation(string username)
    {
        try
        {
            var user = await gitHubClient.User.Get(username);
            return user.Location; ;
        }
        catch (NotFoundException)
        {
            throw new ArgumentException($"User '{username}' not found on GitHub.");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving user bio for '{username}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets detailed information about a GitHub user including bio
    /// </summary>
    /// <param name="username">The GitHub username to look up</param>
    /// <returns>A UserInfo object containing user details</returns>
    public async Task<UserInfo> GetUserInfoAsync(string username)
    {
        try
        {
            var user = await gitHubClient.User.Get(username);
            return new UserInfo
            {
                Username = user.Login,
                Name = user.Name,
                Bio = user.Bio,
                Company = user.Company,
                Location = user.Location,
                Email = user.Email,
                Blog = user.Blog,
                PublicRepos = user.PublicRepos,
                Followers = user.Followers,
                Following = user.Following,
                CreatedAt = user.CreatedAt,
                AvatarUrl = user.AvatarUrl
            };
        }
        catch (NotFoundException)
        {
            throw new ArgumentException($"User '{username}' not found on GitHub.");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving user information for '{username}': {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Represents GitHub user information
/// </summary>
public class UserInfo
{
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Bio { get; set; }
    public string? Company { get; set; }
    public string? Location { get; set; }
    public string? Email { get; set; }    public string? Blog { get; set; }
    public int PublicRepos { get; set; }
    public int Followers { get; set; }
    public int Following { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? AvatarUrl { get; set; }
}