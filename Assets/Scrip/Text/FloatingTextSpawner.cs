using UnityEngine;
using TMPro;

public class FloatingTextSpawner : MonoBehaviour
{
    public static FloatingTextSpawner Instance;

    [SerializeField] private GameObject floatingTextPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SpawnText(string message, Vector3 worldPosition, float duration = 1.5f)
    {
        if (floatingTextPrefab == null) return;

        GameObject textObj = Instantiate(floatingTextPrefab, worldPosition + Vector3.up * 1.2f, Quaternion.identity);
        textObj.GetComponentInChildren<TextMeshProUGUI>().text = message;
        Destroy(textObj, duration);
    }
}
