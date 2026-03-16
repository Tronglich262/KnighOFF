using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public static BGMPlayer Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }
}
