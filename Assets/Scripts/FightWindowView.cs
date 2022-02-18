using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightWindowView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _countMoneyText;

    [SerializeField]
    private TMP_Text _countHealthText;

    [SerializeField]
    private TMP_Text _countPowerText;

    [SerializeField]
    private TMP_Text _crimeLevelText;

    [SerializeField]
    private TMP_Text _countPowerEnemyText;


    [SerializeField]
    private Button _addMoneyButton;

    [SerializeField]
    private Button _minusMoneyButton;


    [SerializeField]
    private Button _addHealthButton;

    [SerializeField]
    private Button _minusHealthButton;


    [SerializeField]
    private Button _addPowerButton;

    [SerializeField]
    private Button _minusPowerButton;

    [SerializeField]
    private Button _minusCrimeLevelButton;

    [SerializeField]
    private Button _fightButton;

    [SerializeField]
    private Button _runButton;

    [SerializeField]
    private Toggle _gunToggle;

    [SerializeField]
    private Toggle _knifeToogle;

    private IEnemy _enemy;

    private IMoneyData _money;
    private IHealthData _health;
    private IPowerData _power;
    private IOverallMoneyData _overallMoney;
    private ICrimeLevelData _crimeLevel;

    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountPowerPlayer;
    private int _allCrimeLevel;

    private int _winsCount;

    private void Start()
    {
        _enemy = new Enemy("Flappy");

        _health = new Health(nameof(Health));
        _health.Attach(_enemy);

        _power = new Power(nameof(Power));
        _power.Attach(_enemy);

        _money = new Money(nameof(Money));

        _overallMoney = new OverallMoney(nameof(OverallMoney));
        _overallMoney.Attach(_enemy);

        _crimeLevel = new CrimeLevel(nameof(CrimeLevel));
        _crimeLevel.Attach(_enemy);

        _addMoneyButton.onClick.AddListener(() => ChangeMoney(true));
        _minusMoneyButton.onClick.AddListener(() => ChangeMoney(false));

        _addHealthButton.onClick.AddListener(() => ChangeHealth(true));
        _minusHealthButton.onClick.AddListener(() => ChangeHealth(false));

        _addPowerButton.onClick.AddListener(() => ChangePower(true));
        _minusPowerButton.onClick.AddListener(() => ChangePower(false));

        _minusCrimeLevelButton.onClick.AddListener(() => ChangeCrimeLevel(false, true));

        _gunToggle.onValueChanged.AddListener(ChangeFightStateOnGun);
        _knifeToogle.onValueChanged.AddListener(ChangeFightStateOnKnife);

        _fightButton.onClick.AddListener(Fight);
        _runButton.onClick.AddListener(Run);
    }

    private void OnDestroy()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();

        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();

        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();

        _minusCrimeLevelButton.onClick.RemoveAllListeners();

        _fightButton.onClick.RemoveAllListeners();
        _runButton.onClick.RemoveAllListeners();

        _gunToggle.onValueChanged.RemoveAllListeners();
        _knifeToogle.onValueChanged.RemoveAllListeners();

        _overallMoney.Detach(_enemy);
        _health.Detach(_enemy);
        _power.Detach(_enemy);
        _crimeLevel.Detach(_enemy);
    }

    private void Fight()
    {
        if (_allCountPowerPlayer >= _enemy.Power)
        {
            Debug.Log("Win");
            _winsCount++;
            if (_winsCount % 5 == 0)
            {
                ChangeCrimeLevel(true, false);
            }
        }
        else
        {
            Debug.Log("Lose");
            ChangeCrimeLevel(false, false);
        }
    }

    private void Run()
    {
        ChangeCrimeLevel(true, false);
    }

    private void ChangePower(bool isAddCount)
    {
        if (isAddCount)
        {
            if (_allCountMoneyPlayer > 0)
            {
                _allCountPowerPlayer++;
                ChangeMoney(false);
            }
        }
        else
        {
            if (_allCountPowerPlayer > 0)
            {
                _allCountPowerPlayer--;
            }
        }
        ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    }

    private void ChangeHealth(bool isAddCount)
    {
        if (isAddCount)
        {
            if (_allCountMoneyPlayer > 0)
            {
                _allCountHealthPlayer++;
                ChangeMoney(false);
            }
        }
        else
        {
            if (_allCountHealthPlayer > 0)
            {
                _allCountHealthPlayer--;
            }
        }
        ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
    }

    private void ChangeMoney(bool isAddCount)
    {
        if (isAddCount)
        {
            _allCountMoneyPlayer++;
            ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
            ChangeDataWindow(_allCountMoneyPlayer, DataType.OverallMoney);
        }
        else
        {
            if (_allCountMoneyPlayer > 0)
            {
                _allCountMoneyPlayer--;
            }
            ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
        }
    }

    private void ChangeCrimeLevel(bool isAddCount, bool isBuy)
    {
        if(isAddCount)
        {
            if (_allCrimeLevel < 5)
            _allCrimeLevel++;
        }
        else
        {
            if(_allCrimeLevel > 0)
            {
                if(isBuy)
                {
                    BuyCrimeLevel();
                }
                else _allCrimeLevel--;
            }
        }

        ChangeDataWindow(_allCrimeLevel, DataType.CrimeLevel);
    }

    private void ChangeFightStateOnKnife(bool isKnife)
    {
        if (isKnife)
        {
            _gunToggle.isOn = false;
            _enemy.ChangeFightState(FightStates.Knife);
            ChangeDataWindow();
        }
    }

    private void ChangeFightStateOnGun(bool isGun)
    {
        if (isGun)
        {
            _knifeToogle.isOn = false;
            _enemy.ChangeFightState(FightStates.Gun);
            ChangeDataWindow();
        }
    }

    private void BuyCrimeLevel()
    {
        var cost = 0;
        switch (_allCrimeLevel)
        {
            case 1:
            case 2:
                cost = 5;
                break;
            case 3:
            case 4:
            case 5:
                cost = 10;
                break;
        }

        if(_allCountMoneyPlayer < cost)
        {
            Debug.Log($"Not enought money, curent cost: {cost}");
        }
        else
        {
            _allCrimeLevel--;
            _allCountMoneyPlayer -= cost;
            ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
        }
    }

    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _countMoneyText.text = $"Player Money: {countChangeData}";
                _money.CountMoney = countChangeData;
                break;

            case DataType.Health:
                _countHealthText.text = $"Player Health: {countChangeData}";
                _health.CountHealth = countChangeData;
                break;

            case DataType.Power:
                _countPowerText.text = $"Player Power: {countChangeData}";
                _power.CountPower = countChangeData;
                break;
            case DataType.OverallMoney:
                _overallMoney.OverallMoneyCount++;
                break;
            case DataType.CrimeLevel:
                _crimeLevelText.text = $"Crime Level: {countChangeData}";
                _crimeLevel.CrimeLevel = countChangeData;
                if (_allCrimeLevel > 2) _runButton.interactable = false;
                else _runButton.interactable = true;
                break;
        }

        _countPowerEnemyText.text = $"Enemy Power: {_enemy.Power}";
    }

    private void ChangeDataWindow()
    {
        _countPowerEnemyText.text = $"Enemy Power: {_enemy.Power}";
    }
}
