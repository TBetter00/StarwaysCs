using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUI;
    public ItemSlot[] itemSlots;
    [Header("Debug")]
    [SerializeField]
    Item itemTest;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggleInventoryUI();
        }
    }

    void toggleInventoryUI()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
    public bool AddItem(Item item)
    {
        foreach (ItemSlot i in itemSlots)
        {
            if (!i.hasItem)
            {
                Debug.Log("Add in manager");
                inventoryUI.SetActive(true);
                i.AddItem(item);
                return true;
            }
        }
        return false;
    }

    public void addTestItem()
    {
        AddItem(itemTest);
    }
}
