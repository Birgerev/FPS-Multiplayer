using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database
{
    public static string GenerateToken(int userId)
    {
        return userId+"";
    }

    public static bool VerifyToken(string token)
    {
        return true;
    }

    public static int GetIdFromToken(string token)
    {
        return 1;
    }
}
