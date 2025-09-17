using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [Header("Prefab và nơi chứa")]
    public GameObject resultPrefab;   // Prefab panel kết quả

    public void ShowResult(Sprite icon, string text)
    {
        if (resultPrefab == null)
        {
            Debug.LogError("Thiếu resultPrefab hoặc parent trong ResultPanel!");
            return;
        }

        // Tạo mới prefab làm con của parent
        GameObject go = Instantiate(resultPrefab, transform.position, Quaternion.identity);

        // Gán icon và text (tìm theo tên con)
        Image iconImg = go.transform.Find("new/Panel/Icon")?.GetComponent<Image>();
        TextMeshProUGUI txt = go.transform.Find("new/Panel/Text")?.GetComponent<TextMeshProUGUI>();

        if (iconImg != null)
            iconImg.sprite = icon;
        else
            Debug.LogWarning("Không tìm thấy 'Icon' trong prefab!");

        if (txt != null)
            txt.text = text;
        else
            Debug.LogWarning("Không tìm thấy 'Text' trong prefab!");

        go.SetActive(true);
        ActiveVongQuay.instance.CloseSpin();
        if(go == null)
        {
            ActiveVongQuay.instance.OpenSpin();
        }
    }
    public void Close() => resultPrefab.SetActive(false);
}
