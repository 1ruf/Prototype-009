using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorSetting : MonoBehaviour
{
    [SerializeField] private Slider _rSlider, _gSlider, _bSlider;
    [SerializeField] private Image _sampleColorImg;
    [SerializeField] private SettingCustomSO _CsSO;

    public UnityEvent OnSettingApply;

    private Color _color;

    private void Start()
    {
        ValueSet();
        ColorSet();
        _rSlider.onValueChanged.AddListener( delegate { ColorSet(); });
        _gSlider.onValueChanged.AddListener( delegate { ColorSet(); });
        _bSlider.onValueChanged.AddListener( delegate { ColorSet(); });
    }
    private void ValueSet()
    {
        Color savedColor = MenuManager.Instance.GameSettingManager.GetColor() / 255f;
        _rSlider.value = savedColor.r;
        _gSlider .value = savedColor.g;
        _bSlider .value = savedColor.b;
    }
    private void ColorSet()
    {
        _color = new Color(_rSlider.value, _gSlider.value, _bSlider.value);
        _sampleColorImg.color = _color;
    }

    public void Apply()
    {
        _CsSO.BackgroundColor = _color;
        OnSettingApply?.Invoke();
    }
}
