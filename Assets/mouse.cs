using UnityEngine;
using UnityEngine.UI;

public class mouse : MonoBehaviour
{
    public GameObject[] otherButtons; // 5 button cần ẩn/hiện
    public Player1 playerScript;      // Script Player cần bật/tắt
    private bool isActive = true;     // Trạng thái hiện tại
    public GameObject pcsetting;

    [Header("Trạng thái check")]
    public bool checkSetting  
    {
        get
        {
            return pcsetting != null && pcsetting.activeSelf;
        }
    }

    public static mouse Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ToggleActive);
    }

    private void ToggleActive()
    {
        isActive = !isActive;

        bool anyButtonInactive = false;

        foreach (GameObject btn in otherButtons)
        {
            if (btn != null)
            {
                btn.SetActive(!isActive); // Đảo trạng thái
                if (!btn.activeSelf)      // Nếu sau khi bị tắt
                {
                    anyButtonInactive = true;
                }
            }
        }

        if (pcsetting != null)
        {
            pcsetting.SetActive(anyButtonInactive);
        }

        if (playerScript != null)
        {
            playerScript.enabled = isActive;
        }
    }
}
