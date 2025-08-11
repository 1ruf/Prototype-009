using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonActions : MonoBehaviour
{
    public void OnPlayPressed()
    {
        Handheld.Vibrate();
        SceneManager.LoadScene("GameScene");
    }
    public void OnQuitPressed() => Application.Quit();
}
