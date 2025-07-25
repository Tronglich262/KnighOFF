using Unity.Jobs;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    public GameObject PanelWin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PanelWin != null)
                    {
            PanelWin.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var check = GetComponent<DialogueTrigger>();
        if (check != null && check.checkwin == 2)
        {
            PanelWin.SetActive(true);
            check.checkwin = 0;
            Debug.Log("da chay");
        }
    }

}
