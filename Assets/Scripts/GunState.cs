using UnityEngine;

public class GunState: IFightState
{
    public void OnEnter()
    {
        Debug.Log("Going into Gun State");
    }
    
    public int GetPower(int playerPower, int playerHealth, int playerOverallMoney, int crimeLevelModifer)
    {
        var plaerPowerModifer = (playerPower + playerHealth) == 0 ? 1 : (playerPower + (2 * playerHealth)) * 0.3f;
        var power = playerOverallMoney * crimeLevelModifer / plaerPowerModifer;
        return (int)power;
    }
}