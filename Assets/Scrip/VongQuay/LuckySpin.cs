using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckySpin : MonoBehaviour
{
    public List<Button> cells;          // List các ô (Button)
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;

    public Button spinButton;
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
            // Reset màu tất cả
            foreach (var cell in cells)
                cell.image.color = normalColor;

            // Highlight ô hiện tại
            cells[index].image.color = highlightColor;

            // Tăng index
            index = (index + 1) % cells.Count;

            // Delay và tăng dần thời gian để tạo hiệu ứng chậm lại
            yield return new WaitForSeconds(delay);
            delay += 0.01f;
        }

        // Ô trúng thưởng chính là ô trước khi index ++
        int finalIndex = (index - 1 + cells.Count) % cells.Count;
        Debug.Log("Dừng tại ô: " + finalIndex);

        // Đổi màu ô cuối để giữ highlight
        foreach (var cell in cells)
            cell.image.color = normalColor;
        cells[finalIndex].image.color = Color.green;

        isSpinning = false;
    }
}
