using System.Collections;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject box1; 
    public GameObject box2;

    void Start()
    {
        StartCoroutine(BoxLoop());
    }

    IEnumerator BoxLoop()
    {
        while (true) 
        {
            box1.SetActive(true);
            yield return new WaitForSeconds(5f);
            box1.SetActive(false);
            box2.SetActive(true);
            yield return new WaitForSeconds(5f);
            box2.SetActive(false);
        }
    }
}