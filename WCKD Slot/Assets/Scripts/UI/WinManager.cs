using TMPro;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    [SerializeField] private SlotMachine _slotMachine;
    [SerializeField] private TextMeshProUGUI _playAmountTMP;
    [SerializeField] private TextMeshProUGUI _balanceTMP;
    private float _playAmount = 1f;
    private float _balance = 1000f;

    private void Start()
    {
        _slotMachine.MaxWinEvent += UpdateBalance;
    }

    public void OnSpinClick()
    {
        if (SlotMachine.State != State.Ready)
        {
            print($"Cannot spin while state is \"{SlotMachine.State}\"");
            return;
        }

        if (!(_balance >= _playAmount))
        {
            print($"Insufficient balance to spin");
            return;
        }

        _balance -= _playAmount;
        _balanceTMP.SetText($"{_balance:C}");


        // each symbol has a y difference of ~1.0673333 between each other
        _slotMachine.PopulateReels(Reel.Row1UpperY, Reel.Row2UpperY, Reel.Row3UpperY, Reel.Row4UpperY);

        SlotMachine.State = State.Spinning;
        // TODO: play spin sound effect
        print($"Spin button clicked! New state: {SlotMachine.State}");


        StartCoroutine(_slotMachine.SpinReels());
    }

    public void UpdateBalance(float multiplier)
    {
        _balance += _playAmount * multiplier;
        _balanceTMP.SetText($"{_balance:C}");
    }

    public void OnIncrementClick()
    {
        print("Increasing play amount");

        switch (_playAmount)
        {
            case 1:
                _playAmount = 5;
                break;
            case 5:
                _playAmount = 10;
                break;
            case 10:
                _playAmount = 100;
                break;
            case 100:
                _playAmount = 1000;
                break;
        }

        _playAmountTMP.SetText($"{_playAmount:C}");
    }

    public void OnDecrementClick()
    {
        print("Increasing play amount");

        switch (_playAmount)
        {
            case 5:
                _playAmount = 1;
                break;
            case 10:
                _playAmount = 5;
                break;
            case 100:
                _playAmount = 10;
                break;
            case 1000:
                _playAmount = 100;
                break;
        }

        _playAmountTMP.SetText($"{_playAmount:C}");
    }

    private void OnDisable()
    {
        _slotMachine.MaxWinEvent -= UpdateBalance;
    }
}