using UnityEngine;

public class PlayerCollectItem : MonoBehaviour
{
    // public InventoryManager inventoryManager;
    void Awake()
    {
        // inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    bool Collect(Item item)
    {
        if (item == null) return false;
        return InventoryManager.instance.AddItem(item);
        // return true;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the object we bumped into has the ItemObject component
        if (hit.gameObject.TryGetComponent<ItemObject>(out ItemObject itemObj))
        {
            Item collectedItem = itemObj.Collected();

            if (Collect(collectedItem))
            {
                Debug.Log($"Collected: {collectedItem.itemName}");
                // Only destroy if it was successfully added to inventory
                Destroy(itemObj.gameObject);
            }
            else
            {
                Debug.Log("Inventory Full or Item Null.");
            }
        }
    }
}
