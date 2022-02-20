public class EnemyFightStateHandler
{
    private IEnemyWithFightState _enemy;

    private KnifeState _knifeState;
    private GunState _gunState;
    public EnemyFightStateHandler(IEnemyWithFightState enemy)
    {
        _enemy = enemy;

        _knifeState = new KnifeState();
        _gunState = new GunState();
    }

    public void SetKnifeState()
    {
        _knifeState.OnEnter();
        _enemy.FightState = _knifeState;
    }

    public void SetGunState()
    {
        _gunState.OnEnter();
        _enemy.FightState = _gunState;
    }
}