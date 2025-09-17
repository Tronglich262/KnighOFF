using UnityEngine;

public class ActiveVongQuay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject spin;
    public static ActiveVongQuay instance;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
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
    public void ActiveSpin() => spin.SetActive(true);

    public void CLoseSpin1() => spin.SetActive(false);


    //active singleton pattern  ( liên kết tới spin)
    public void CloseSpin() => spin.SetActive(false);

    public void OpenSpin() => spin.SetActive(true);

}
