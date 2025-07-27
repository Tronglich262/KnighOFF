using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

public class mouse : MonoBehaviour
{
    public GameObject[] otherButtons; // 5 button cần ẩn/hiện
    public Player1 playerScript; // Script Player cần bật/tắt
    private bool isActive = true; // Trạng thái hiện tại
    public bool CheckScrip = false;
    public GameObject pcsetting;
    private void Start()
    {
        // Gán sự kiện bấm vào button
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

        // Nếu có bất kỳ button nào đang tắt → bật pcsetting
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
