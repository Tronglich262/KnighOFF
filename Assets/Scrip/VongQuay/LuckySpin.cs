using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

public class LuckySpin : MonoBehaviour
{
    [Header("Cấu hình vòng quay")]
    public List<ItemVongQuay> cells;  // danh sách các ô (ItemVongQuay)
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;
    public Button spinButton;
    public ResultPanel resultPanel;   // tham chiếu tới panel kết quả

    [Header("UI Components")]
    public TextMeshProUGUI TextVongQuay;
    public GameObject TextdebugVongQuay;
    public int soLuotQuay = 0;
    private bool isSpinning = false;

    [Header("Singleton")]
    public static LuckySpin instance;
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
        spinButton.onClick.AddListener(StartSpin);
        TextVongQuay.text = $"Số lượt quay: {soLuotQuay}";

    }

    void StartSpin()
    {
        if (!isSpinning && soLuotQuay > 0)
            StartCoroutine(SpinRoutine());
        else
            StartCoroutine(debugvongquay());
    }
    IEnumerator debugvongquay()
    {
        GameObject instance = Instantiate(TextdebugVongQuay, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(instance);
    }
    IEnumerator SpinRoutine()
    {
        isSpinning = true;
        Userpin();
        int index = 0;
        int totalSteps = Random.Range(20, 40);  // số bước chạy ngẫu nhiên
        float delay = 0.05f;                    // tốc độ ban đầu

        for (int i = 0; i < totalSteps; i++)
        {
            // reset màu tất cả
            foreach (var cell in cells)
                cell.iconImage.color = normalColor;

            // highlight ô hiện tại
            cells[index].iconImage.color = highlightColor;

            index = (index + 1) % cells.Count;

            yield return new WaitForSeconds(delay);
            delay += 0.01f;
        }

        // Lấy ô trúng thưởng
        int finalIndex = (index - 1 + cells.Count) % cells.Count;
        ItemVongQuay winner = cells[finalIndex];

        Debug.Log("Dừng tại ô: " + winner.text);

        // reset màu và giữ highlight ô thắng
        foreach (var cell in cells)
            cell.iconImage.color = normalColor;
        winner.iconImage.color = Color.green;

        if (resultPanel != null)
        {
            PhanThuong reward = winner.GetComponent<PhanThuong>();
            resultPanel.ShowResult(winner.icon, winner.text, reward);
        }

        isSpinning = false;
    }
    public void AddSpinCount(int amount)
    {
        soLuotQuay = soLuotQuay + amount;
        TextVongQuay.text = $"Số lượt quay: {soLuotQuay}";
    }
    public void Userpin()
    {
        if(soLuotQuay > 0)
        {
            soLuotQuay--;
            TextVongQuay.text = $"Số lượt quay: {soLuotQuay}";
        }
    }
}
