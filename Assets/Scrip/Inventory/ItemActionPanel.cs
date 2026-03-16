using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;

        public void AddButon(string name, Action onClickAction)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
    
            Button uiButton = button.GetComponent<Button>();
            if (uiButton != null)
            {
                Debug.Log($"Button {name} created");  
                uiButton.onClick.AddListener(() => {
                    Debug.Log($"Button {name} clicked");  
                    onClickAction?.Invoke();
                    Toggle(false);
                });
            }
            else
            {
                Debug.LogError("Không tìm thấy Button component trong buttonPrefab!");
            }

            button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
        }


        internal void Toggle(bool val)
        {
            if (val == true)
                RemoveOldButtons();
            gameObject.SetActive(val);
        }

        public void RemoveOldButtons()
        {
            foreach (Transform transformChildObject in transform)
            {
                Destroy(transformChildObject.gameObject);
            }
        }
    }
}