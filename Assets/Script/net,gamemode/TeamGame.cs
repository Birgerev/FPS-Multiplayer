using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class TeamGame : Gamemode
{
    public List<Team> teams = new List<Team>();

    [ClientRpc]
    public override void Rpc_onStart()
    {
        base.Rpc_onStart();
    }

    public override void Start()
    {
        base.Start();

        teams[0] = new Team("USA");
        teams[1] = new Team("Germany");
    }
}

[System.Serializable]
public class Team
{
    public string name = "Mexico";
    
    public List<int> players = new List<int>();


    public Team(string name)
    {
        this.name = name;
    }
}

private class PlayerData(){

    }
