using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    public Item item;
    public Image itemImage;
    public bool hasItem;
    void Awake()
    {
        this.itemImage.gameObject.SetActive(false);
    }

    public void AddItem(Item item)
    {
        Debug.Log("Add in slot");
        this.item = item;
        this.itemImage.sprite = item.itemSprite;
        refreshDisplay();
    }

    public void RemoveItem()
    {
        this.item = null;
        refreshDisplay();
    }

    public void refreshDisplay()
    {
        itemImage.gameObject.SetActive(item);
        hasItem = item;
    }
}
