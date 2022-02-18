using UnityEngine;

public class KnifeState: IFightState
{
    public void OnEnter()
    {
        Debug.Log("Going into Knife State");
    }
    public int GetPower(int playerPower, int playerHealth, int playerOverallMoney, int crimeLevelModifer)
    {
        var plaerPowerModifer = (playerPower + playerHealth) == 0 ? 1 : ((2 * playerPower) + playerHealth) * 0.3f;
        var power = playerOverallMoney * crimeLevelModifer / plaerPowerModifer;
        return (int)power;
    }
}