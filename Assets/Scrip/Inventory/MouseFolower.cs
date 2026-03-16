using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Inventory.UI;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private UiinventoryItem Item;

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        Item = GetComponentInChildren<UiinventoryItem>();
        
    }

    public void SetData(Sprite sprite, int quantity)
    {
        Item.SetData(sprite, quantity);
    }

    void Update()
    {
        if (gameObject.activeSelf) 
        {
            transform.position = Input.mousePosition; 
        }
    }

    public void Toggle(bool val)
    {
        Debug.Log($"Item toggle {val}");
        gameObject.SetActive(val);
    }
}