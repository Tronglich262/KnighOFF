using UnityEngine;
using UnityEngine.UI; 

public class Buildo : MonoBehaviour
{
    public GameObject builddo; 
    public Button openButton;  

    private bool isOpen = false; 
    void Start()
    {
        if (builddo != null)
        {
            builddo.SetActive(false); 
        }

        if (openButton != null)
        {
            openButton.onClick.AddListener(ToggleBuilddo);
        }
    }

    void ToggleBuilddo()
    {
        isOpen = !isOpen;
        builddo.SetActive(isOpen);
    }
}