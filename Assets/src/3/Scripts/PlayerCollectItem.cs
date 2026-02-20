using UnityEditor.AssetImporters;
using UnityEngine;

public class PlayerCollectItem : MonoBehaviour
{
    public InventoryManager inventoryManager;
    void Awake()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    void Collect(Item item)
    {
        inventoryManager.AddItem(item);
    }

    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.TryGetComponent<ItemObject>(out ItemObject itemObj))
        {
            Item collectedItem = /* Item <- */itemObj.Collected();
            Collect(collectedItem);
            Destroy(itemObj.gameObject);
        }
        else
        {
            // Debug.Log("No item object here");
        }
    }
}
