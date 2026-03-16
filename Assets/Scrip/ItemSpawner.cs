using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    
    public GameObject lootPrefab; 
    public float spawnInterval = 3f; 
    public float fallSpeed = 5f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SpawnItemRoutine());
    }

    IEnumerator SpawnItemRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            GameObject spawnedItem = Instantiate(lootPrefab, transform.position, Quaternion.Euler(0, 0, -90));
            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();

            if (rb == null)
            {
                rb = spawnedItem.AddComponent<Rigidbody2D>();
            }
            rb.gravityScale = 1; 
            rb.linearVelocity = new Vector2(0, -fallSpeed); 
        }
    }
}
