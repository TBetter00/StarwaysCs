using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject inventoryUI;
    public ItemSlot[] itemSlots;
    [Header("Debug")]
    [SerializeField]
    Item itemTest;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

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

    public void DeselectedAllSlot()
    {
        foreach (ItemSlot i in itemSlots)
        {
            i.OnDeselected();
        }
    }
    /// <summary>
    /// For Debug
    /// </summary>
    public void addTestItem()
    {
        AddItem(itemTest);
    }


}
