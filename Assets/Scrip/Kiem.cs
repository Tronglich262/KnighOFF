using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Kiem : MonoBehaviour
{
    public int damage = 10;
    public BoxCollider2D swordCollider;
    public GameObject damageTextPrefab;
    public Button attackButton;

    public Animator playerAnimator; // Thêm tham chiếu Animator từ Player

    private void Start()
    {
        swordCollider.enabled = false;

        if (attackButton != null)
        {
            attackButton.onClick.AddListener(Attack);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("guomattack");
        }
        else
        {
            Debug.LogWarning("Thiếu Animator của Player trong Kiem.cs");
        }
    }

    public void EnableSwordCollider()
    {
        swordCollider.enabled = true;
        StartCoroutine(DisableSwordCollider());
    }

    IEnumerator DisableSwordCollider()
    {
        yield return new WaitForSeconds(0.2f);
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 spawnPos = other.transform.position + new Vector3(0, 1, 0);
            Instantiate(damageTextPrefab, spawnPos, Quaternion.identity).GetComponent<DamagePopup>().Setup(damage);

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Destroy(other.gameObject);
            }

            Debug.Log("Chém trúng quái!");
        }
    }
}
