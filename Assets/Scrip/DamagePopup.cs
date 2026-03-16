using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI textMesh; 
    public float destroyTime = 0.5f;
    public Vector3 moveSpeed = new Vector3(0, 1f, 0);

    public void Setup(int damage)
    {
        if (textMesh == null)
        {
            Debug.LogError("TextMeshPro chưa được gán!");
            return;
        }
        textMesh.text = damage.ToString(); 
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime;
    }
}