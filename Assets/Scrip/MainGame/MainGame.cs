using UnityEngine;

public class MainGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        Time.timeScale = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
