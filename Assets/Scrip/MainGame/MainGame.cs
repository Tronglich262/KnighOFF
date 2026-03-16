using UnityEngine;

public class MainGame : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        Time.timeScale = 2f;
    }
}
