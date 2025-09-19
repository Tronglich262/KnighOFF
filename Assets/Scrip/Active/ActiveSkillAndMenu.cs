using UnityEngine;

public class ActiveSkillAndMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("tắt btn all để hiển check map")]
    public GameObject btnall;
    public static ActiveSkillAndMenu instance;
    public void Awake()
    {
        if (instance == null)
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activebtnall()
    {
        btnall.SetActive(false);
    }
    public void unactivebtnall()
    {
        btnall.SetActive(true);
    }
}
