using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TestButton : MonoBehaviour
{
    public Button testButton;

    void Start()
    {
        if (testButton != null)
        {
            testButton.onClick.RemoveAllListeners();
            testButton.onClick.AddListener(() => Debug.Log("✅ Yeni Test Butonu ÇALIŞTI!"));
        }
        else
        {
            Debug.LogError("❌ Test butonu atanmadı! Inspector'da butonu sürükleyip bırakın.");
        }
    }
}