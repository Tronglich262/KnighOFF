using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckySpin : MonoBehaviour
{
    public List<ItemVongQuay> cells;  // danh sách các ô (ItemVongQuay)
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;

    public Button spinButton;
    public ResultPanel resultPanel;   // tham chiếu tới panel kết quả

    private bool isSpinning = false;

    void Start()
    {
        spinButton.onClick.AddListener(StartSpin);
    }

    void StartSpin()
    {
        if (!isSpinning)
            StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        isSpinning = true;

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

        // Gửi dữ liệu sang panel kết quả
        if (resultPanel != null)
            resultPanel.ShowResult(winner.icon, winner.text);

        isSpinning = false;
    }
}
