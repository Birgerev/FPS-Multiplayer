using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameProfile
{
    public string name;
    public int id;
    public string gameToken;

    public Sprite getIcon()
    {
        return null;
    }

    public GameProfile()
    {

    }

    public GameProfile(int id)
    {
        name = "birgere";
        this.id = id;
        gameToken = Database.GenerateToken(id);
    }

    public bool verifyProfile()
    {
        return Database.VerifyToken(gameToken);
    } 
}
