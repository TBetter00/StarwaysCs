using UnityEngine;
[CreateAssetMenu(fileName = "new Item", menuName = "Item")]

public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
}
