using UnityEngine;

public class GameSettingManager : MonoBehaviour
{
    private Color _backgroundColor;
    public void Apply()
    {
        SetBackground(_backgroundColor);
    }

    private void SetBackground(Color color)
    {

    }
}
