using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject setting;
    public GameObject help;

    [Header("button")]
    public GameObject playegame;
    public GameObject settinggame;
    public GameObject helpgame;
    public GameObject quitgame;
    void Start()
    {
        if (menu == null)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        } 
    }

    public void startgame()
    {
        SceneManager.LoadScene("Demo");
    }

    public void ToggleMenu()
    {
        setting.SetActive(true);
        dissactiveallbtn();
    } 
    public void HelpMenu()
    {
        help.SetActive(true);
        dissactiveallbtn();
    }

    public void BackMenu()
    {
        setting.SetActive(false);
        activeallbtn();
    }
    public void HelpBackMenu()
    {
        help.SetActive(false);
        activeallbtn();
    }
    public void QuitGame()
    {
        Application.Quit();
    }


    public void activeallbtn()
    {
        playegame.SetActive(true);
        settinggame.SetActive(true);
        helpgame.SetActive(true);
        quitgame.SetActive(true);
    }
    public void dissactiveallbtn()
    {
        playegame.SetActive(false);
        settinggame.SetActive(false);
        helpgame.SetActive(false);
        quitgame.SetActive(false);
    }

}