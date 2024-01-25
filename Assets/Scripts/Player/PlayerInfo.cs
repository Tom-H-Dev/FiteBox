using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInfo : ScriptableObject
{
    public string playerName;

    public int playerWins;
    public int playerKills;
    public int playerDeaths;
    public float playerKDRatio;
}
