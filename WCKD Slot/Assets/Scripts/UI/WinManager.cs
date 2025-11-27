using TMPro;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playAmountTMP;
    private float _playAmount = 1f;

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
}