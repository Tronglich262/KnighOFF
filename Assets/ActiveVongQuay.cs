using UnityEngine;

public class ActiveVongQuay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject spin;
    void Start()
    {
        if(spin != null)
        {
            spin.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActiveSpin()
    {
       spin.SetActive(!spin.activeSelf);
    }
}
