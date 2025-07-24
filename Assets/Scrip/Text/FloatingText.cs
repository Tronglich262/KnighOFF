using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float fadeSpeed = 1f;

    private TextMeshProUGUI text;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        if (canvasGroup != null)
        {
            canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
        }
    }
}
