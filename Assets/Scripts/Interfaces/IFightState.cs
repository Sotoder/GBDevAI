public interface IFightState
{
    int GetPower(int playerPower, int playerHealth, int playerOverallMoney, float crimeLevelModifer);
}