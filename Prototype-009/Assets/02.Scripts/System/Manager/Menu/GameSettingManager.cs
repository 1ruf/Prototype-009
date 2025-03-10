using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettingManager : MonoBehaviour  //SO의 저장된 값을 Apply 하면 저장되는 방식, 설정에서 변경하면 SO에서 변경된다.
{
    [Header("Targets")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private SpriteRenderer[] _targetSprites;
    [Header("Datas")]
    [SerializeField] private SettingCustomSO _CsSO;//CustomSetting

    private Color _backgroundColor;

    private void OnEnable()
    {
        //Apply();
        GetDatas();
        Set();
    }
    private void GetDatas()
    {
        _backgroundColor = GetColor();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //SO의 저장되지 않은 값들을 PlayerPrefs에 저장
        {
            Apply();
        }
    }
    public void Apply()
    {
        Set();
        SaveColor(_CsSO.BackgroundColor.r * 255, _CsSO.BackgroundColor.g * 255, _CsSO.BackgroundColor.b * 255);
        SceneManager.LoadScene(0);
    }

    public void Set()
    {
        SetBackground(_backgroundColor);
        SetWallColor(_backgroundColor);
    }

    public Color GetColor()
    {
        Color savedColor = new Color(PlayerPrefs.GetFloat("CustomColor_R", _backgroundColor.r), PlayerPrefs.GetFloat("CustomColor_G", _backgroundColor.g), PlayerPrefs.GetFloat("CustomColor_B", _backgroundColor.b), 255f);
        print($"color get request applied : {savedColor}");
        return savedColor;
    }
    private void SaveColor(float r,float g,float b)
    {
        PlayerPrefs.SetFloat("CustomColor_R", r);
        PlayerPrefs.SetFloat("CustomColor_G", g);
        PlayerPrefs.SetFloat("CustomColor_B", b);
        print($"color save request applied : ({r},{g},{b})");
    }
    private void SetWallColor(Color color)
    {
        foreach (SpriteRenderer wall in _targetSprites)
        {
            wall.color = (color / 255f);
        }
    }
    private void SetBackground(Color color)
    {
        _mainCamera.backgroundColor = (color / 255f);// background setting
    }
}
