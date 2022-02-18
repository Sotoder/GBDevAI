public interface IEnemy
{
    void Update(DataPlayer dataPlayer, DataType dataType);
    void ChangeFightState(FightStates state);
    int Power { get; }
}
