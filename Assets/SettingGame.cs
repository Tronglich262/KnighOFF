using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingGame : MonoBehaviour
{
   public GameObject menu;
   public GameObject setting;
   public GameObject help;
    public TextMeshProUGUI fpsText;
    private float deltaTime;

    private void Start()
    {
        Application.targetFrameRate = 240;
        QualitySettings.vSyncCount = 0; // Tắt V-Sync để FPS limit có hiệu lực
    }
    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        float fps = 1.0f / deltaTime;
        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
    }

    public void MenuGame()
   {
      menu.SetActive(!menu.activeSelf);
   }

   public void SettingGame1()
   {
      menu.SetActive(!setting.activeSelf);
      setting.SetActive(true);
   }

   public void HelpGame()
   {
      menu.SetActive(!menu.activeSelf);
      help.SetActive(true);
   }

   public void SettingBackToMenu()
   {
      setting.SetActive(!setting.activeSelf);
      menu.SetActive(true);
   }
   public void HelpBackToMenu()
   {
      help.SetActive(false);
      menu.SetActive(true);
   }

   public void backmenu()
   {
      menu.SetActive(!menu.activeSelf);
   }
   public void Back()
    {
        SceneManager.LoadScene("MENU");
            ScoreManager.Instance.resetScore();
    }
}
