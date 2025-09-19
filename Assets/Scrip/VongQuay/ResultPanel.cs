using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [Header("Prefab và nơi chứa")]
    public GameObject resultPrefab;   // Prefab panel kết quả
    public AudioClip spawnSound;
    public void ShowResult(Sprite icon, string text, PhanThuong phanThuong)
    {
        if (resultPrefab == null)
        {
            Debug.LogError("Thiếu resultPrefab hoặc parent trong ResultPanel!");
            return;
        }

        // Tạo mới prefab làm con của parent
        GameObject go = Instantiate(resultPrefab, transform.position, Quaternion.identity);
        //  Thêm AudioSource và phát âm thanh
        AudioSource audio = go.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        audio.clip = spawnSound;
        audio.Play();

        // Gán icon và text (tìm theo tên con)
        Image iconImg = go.transform.Find("new/Panel/Icon")?.GetComponent<Image>();
        TextMeshProUGUI txt = go.transform.Find("new/Panel/Text")?.GetComponent<TextMeshProUGUI>();
        Button claimBtn = go.transform.Find("new/Panel/ClaimButton")?.GetComponent<Button>();

        if (iconImg != null)
            iconImg.sprite = icon;
        else
            Debug.LogWarning("Không tìm thấy 'Icon' trong prefab!");

        if (txt != null)
            txt.text = text;
        if (claimBtn != null)
        {
            // Xóa listener cũ rồi add mới
            claimBtn.onClick.RemoveAllListeners();
            claimBtn.onClick.AddListener(() => phanThuong.NhanThuong());
            claimBtn.onClick.AddListener(() => Destroy(go)); // đóng panel sau khi nhận
        }
        else
            Debug.LogWarning("Không tìm thấy 'Text' trong prefab!");

        go.SetActive(true);
        ActiveVongQuay.instance.CloseSpin();
        if(go == null)
        {
            ActiveVongQuay.instance.OpenSpin();
        }
    }
}
