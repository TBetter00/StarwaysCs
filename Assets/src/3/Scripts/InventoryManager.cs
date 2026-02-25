using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUI; // ตัว UI ใน inspector
    public ItemSlot[] itemSlots;
    [Header("Debug นะจ๊ะ")]
    public Item testItem;
    void Awake()
    {
        inventoryUI.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggleInventoryUI();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddTestItem();
        }
    }

    void toggleInventoryUI()
    {
        // ~ นิเสธ ถ้า a -> true แล้ว ~a -> false หรือ ถ้า b -> false แล้ว ~b -> true หงายมือ ควำ่มือ
        inventoryUI.SetActive(!inventoryUI.activeSelf/*ค่าความจริงว่า gameobject เปิดอยู่ หรือ ปิดอยู่*/);
    }

    public void AddItem(Item item)
    {
        foreach (ItemSlot i in itemSlots)
        {
            if (i.hasItem == false)
            {
                i.AddItem(item);
                return;
            }
        }
        Debug.Log("กูคือ inventory ตอนนี้ท้องกูเต็มแล้ว ยัดไม่ได้แล้ว");
    }
    public void AddTestItem()
    {
        AddItem(testItem);
    }
    public void DeselectedAllSlot()
    {
        foreach (ItemSlot i in itemSlots)
        {
            i.Deselected();
        }
    }
}
