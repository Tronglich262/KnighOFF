using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;


public class Xmenu : MonoBehaviour
{
    public GameObject Tatmenu;

    public GameObject bando;

    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar; 
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        if (Tatmenu != null)
        {
            Tatmenu.SetActive(true); 
        }

        if (bando != null)
        {
            bando.SetActive(false); 
        }
    }

    public void ToggleMenu()
    {
        if (Tatmenu != null)
        {
            Tatmenu.SetActive(!Tatmenu.activeSelf);
        }
    }

    public void ToggleBando()
    {
        bando.SetActive(true);
        Tatmenu.SetActive(!Tatmenu.activeSelf);
    }

    public void Tatbando()
    {
        bando.SetActive(false);
        Tatmenu.SetActive(true);
    }

    public void congmau()
    {
        if (healthBar.value < maxHealth)
        {
            healthBar.value += 50;
          
        }
      

        if (healthBar.value > maxHealth)
        {
            healthBar.value = maxHealth;
        }
    }
}
