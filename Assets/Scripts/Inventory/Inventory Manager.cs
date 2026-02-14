using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemSlot[] itemSlots;
    [Header("Debug")]
    [SerializeField]
    Item itemTest;
    void AddItem(Item item)
    {
        foreach (ItemSlot i in itemSlots)
        {
            if (!i.hasItem)
            {
                Debug.Log("Add in manager");
                i.AddItem(item);
            }
        }
    }

    public void addTestItem()
    {
        AddItem(itemTest);
    }
}
