using UnityEngine.UI;
using UnityEngine;
using NUnit.Framework;

public class ItemSlot : MonoBehaviour
{
    public Item item; // ช่องให้สำหรับใส่ item
    public Image itemImage;
    public bool hasItem; // true false
    void Awake()
    {
        this.itemImage.gameObject.SetActive(false); // ปิดรูปตอนเข้า
    }
    // ทำงานทุก frame
    void Update()
    {

    }
    public void AddItem(Item itemPar)
    {
        this.item = itemPar;
        this.itemImage.sprite = item.itemIcon;
        itemImage.gameObject.SetActive(true);
        hasItem = true;
    }
    /*
    สาธาณณะ ที่ว่างเปล่า เพิ่มไอเท็ม(ไอเท็ม ไอเท็มพา)
    {
        นี้.ไอเท็ม = ไอเท็มพา;
        นี้.รูปภาพไอเท็ม.น้ำอัดลม = ไอเท็ม.ไอค่อนไอเท็ม;
        รูปภาพไอเท็ม.วัตถุเกม.ให้ใช้งาน(จริง);
        มีไอเท็ม = จริง;
    }
    */
    public void RemoveItem()
    {
        this.item = null;
        itemImage.gameObject.SetActive(false);
        hasItem = false;
        // TODO: โยงเชื่อม กับระบบ drop item
    }

}
