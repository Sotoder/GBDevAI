using System;
using UnityEngine;

public class Enemy : IEnemy, IEnemyWithFightState
{
    private string _name;

    private int _overallMoneyPlayer;
    private int _healthPlayer;
    private int _powerPlayer;
    private int _crimeLevelPlayer;
    private int _crimeModifier;

    private EnemyFightStateHandler _enemyFightStateHandler;
    private IFightState _fightState;

    public IFightState FightState { get => _fightState; set => _fightState = value; }

    public int Power
    {
        get
        {
            return _fightState.GetPower(_powerPlayer, _healthPlayer, _overallMoneyPlayer, _crimeModifier);
        }
    }

    public Enemy(string name)
    {
        _name = name;
        CalculateCrimeModifer();

        _enemyFightStateHandler = new EnemyFightStateHandler(this);
        _enemyFightStateHandler.SetGunState();
    }

    public void ChangeFightState(FightStates state)
    {
        switch (state)
        {
            case FightStates.Gun:
                _enemyFightStateHandler.SetGunState();
                break;
            case FightStates.Knife:
                _enemyFightStateHandler.SetKnifeState();
                break;
        }
    }

    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Health:
                _healthPlayer = dataPlayer.CountHealth;
                break;

            case DataType.OverallMoney:
                _overallMoneyPlayer = dataPlayer.OverallMoneyCount;
                break;

            case DataType.Power:
                _powerPlayer = dataPlayer.CountPower;
                break;
            case DataType.CrimeLevel:
                _crimeLevelPlayer = dataPlayer.CrimeLevel;
                CalculateCrimeModifer();
                break;
        }

        Debug.Log($"Update {_name}, change {dataType}");
    }

    private void CalculateCrimeModifer()
    {
        switch (_crimeLevelPlayer)
        {
            case 0:
            case 1:
                _crimeModifier = 2;
                break;
            case 2:
                _crimeModifier = 3;
                break;
            case 3:
                _crimeModifier = 5;
                break;
            case 4:
            case 5:
                _crimeModifier = 7;
                break;
        }
    }
}
