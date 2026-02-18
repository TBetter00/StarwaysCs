using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item itemData;
    public Item Collected()
    {
        if (itemData != null) return itemData;
        return null;
    }
}
