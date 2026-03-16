using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Khien : MonoBehaviour
{
    public BoxCollider2D swordCollider;
    public Button shieldButton;
    public Animator playerAnimator; 

    private void Start()
    {
        swordCollider.enabled = false;

        if (shieldButton != null)
        {
            shieldButton.onClick.AddListener(ActivateShield);
        }

        if (playerAnimator == null)
        {
            playerAnimator = GetComponentInParent<Animator>(); 
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ActivateShield();
        }
    }

    public void ActivateShield()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("khien");
        }
        else
        {
            Debug.LogWarning("Player Animator không được gán trong Khien.cs");
        }
    }

    // Hàm gọi từ Animation Event
    public void EnableShieldCollider()
    {
        swordCollider.enabled = true;
        StartCoroutine(DisableColliderAfterDelay());
    }

    IEnumerator DisableColliderAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Đỡ đòn thành công!");
        }
    }
}
