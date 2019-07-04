using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class GameProfile
{
    public string name;
    public int id;
    public string gameToken;

    public bool verified = true;

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
    }

    public GameProfile(string token)
    {
        name = "birgere";
        this.gameToken = token;
    }

    public void verify()
    {
        //TODO
        Database.instance.StartCoroutine(getIdFromToken());
    }

    IEnumerator getIdFromToken()
    {
        UnityWebRequest www = UnityWebRequest.Get(Database.databaseServerAdress + Database.database_getIdFromToken + "/?token=" + gameToken);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            string result = www.downloadHandler.text;
            if (result == "")
                verified = false;
            id = int.Parse(result);
        }
    }

    public void login()
    {
        Database.instance.StartCoroutine(retrieveToken());
    }

    IEnumerator retrieveToken()
    {
        UnityWebRequest www = UnityWebRequest.Get(Database.databaseServerAdress+Database.database_login+"/?player-id="+id);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            gameToken = www.downloadHandler.text;
        }
    }
}
