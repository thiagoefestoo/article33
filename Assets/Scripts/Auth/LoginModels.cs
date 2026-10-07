using System;

[Serializable]
public class LoginRequest
{
    public string username;
    public string password;
}

[Serializable]
public class LoginResponse
{
    public string message;
    public int userId;
    public string username;
}