using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemVongQuay : MonoBehaviour
{
    [Header("Item Data")]
    public Sprite icon;
    public string text;

    [Header("UI Components")]
    public Image iconImage;

    public void SetItem(Sprite icon, string text)
    {
        this.icon = icon;
        this.text = text;

        if (iconImage != null)
            iconImage.sprite = icon;

    }
}
