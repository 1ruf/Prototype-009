using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using KoUtility.Unity.Common;

public class TestBall : MonoBehaviour
{
    SaveData saveData = new SaveData() { Name = "Test", value = 10 };
    private void Start()
    {
        DataSaver.Save(saveData);
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            SaveData data = DataSaver.Load();
            print($"데이터로드 - {data.Name} : {data.value}");
        }
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            saveData.value++;
            DataSaver.Save(saveData);
        }
    }

}
