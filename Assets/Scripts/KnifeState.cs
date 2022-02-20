using System;
using UnityEngine;

public class KnifeState: IFightState
{
    public void OnEnter()
    {
        Debug.Log("Going into Knife State");
    }
    public int GetPower(int playerPower, int playerHealth, int playerOverallMoney, float crimeLevelModifer)
    {
        var playerHealthModifier = playerHealth > 30 ? 1 : 2;
        var playerPowerModifer = (playerPower + playerHealth) == 0 ? 1 : ((2 * playerPower) + playerHealth) * 0.1f;
        var power = Math.Ceiling(((playerOverallMoney * 0.6) + (playerHealth/ playerHealthModifier) + (playerPower / playerPowerModifer)) * crimeLevelModifer);
        return (int)power;
    }
}