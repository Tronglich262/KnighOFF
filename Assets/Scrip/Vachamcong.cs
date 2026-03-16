using System;
using System.Collections;
using UnityEngine;

public class Vachamcong : MonoBehaviour
{
    private Animator animator;
    public GameObject frefab;
    public Transform spawnPoint;
    public GameObject spawnedObject; 

    void Start()
    {
        animator = GetComponent<Animator>();
     
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(spawnnamgiay());
        }
    }


    IEnumerator spawnnamgiay()
    {
        spawnedObject =Instantiate(frefab, spawnPoint.position, Quaternion.identity); 
        yield return new WaitForSeconds(1f);
        Destroy(spawnedObject); 
    }

   
}