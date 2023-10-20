using Newtonsoft.Json;
using System;

public class Users
{
    [JsonProperty("username")]
    public string Username { get; set; } = string.Empty;

    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("password")]
    public string Password { get; set; } = String.Empty;

    [JsonProperty("confirmPassword")]
    public string ConfirmPassword { get; set; } = String.Empty;
}