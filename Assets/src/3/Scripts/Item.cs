using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;
    [Tooltip("เผื่อได้ใช้(ไม่จำเป็นต้องมี)")]
    public string itemDescription;
}
