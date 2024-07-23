﻿namespace Innowise.Common.Services.Email;

public class EmailOptions
{
    public const string Email = "EmailOptions";
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}