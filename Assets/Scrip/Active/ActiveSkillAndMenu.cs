using UnityEngine;

public class ActiveSkillAndMenu : MonoBehaviour
{
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
  
    public void activebtnall()
    {
        btnall.SetActive(false);
    }
    public void unactivebtnall()
    {
        btnall.SetActive(true);
    }
}
