using Inventory.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Mapdautien : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;
    public bool ischeck = false;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ischeck = true;
            //them
            inventoryData.SaveInventory();
            SceneManager.LoadScene("MapDau");

        }
    }
}