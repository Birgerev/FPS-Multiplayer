using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    public const string databaseServerAdress = "http://hille.evansson.se/game_ww2/";
    public const string database_login = "login.php";
    public const string database_getIdFromToken = "get_id_from_token.php";
    
    public static Database instance;

    private void Awake()
    {
        instance = this;
    }
    public static int GetIdFromToken(string token)
    {
        print(token);
        return -1;
    }
}
