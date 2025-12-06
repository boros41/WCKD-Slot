using UnityEngine;
using UnityEngine.UI;


public class TurboSpinButton : MonoBehaviour
{
    [SerializeField] private Image _turboImage;
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    public static float SpinDuration { get; private set; } = 1f;

    private bool _isOn;

    public void OnTurboClick()
    {
        _isOn = !_isOn;

        print(_isOn ? $"Turbo spin is now on!" : $"Turbo spin is now off!");

        SpinDuration = _isOn ? 0.5f : 1f;

        _turboImage.sprite = _isOn ? _onSprite : _offSprite;
    }
}
