using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public Item item; // ช่องให้สำหรับใส่ item
    public Image itemImage;
    public GameObject selectedGraphic;
    [Header("Boolean")]
    public bool hasItem; // true false
    public bool isSelected;
    private InventoryManager inventoryManager;
    [Header("Dragging System")]
    private Canvas rootCanvas; // Cached canvas for coordinate conversion and drag visuals
    private Transform originalItemParent; // Where the icon returns after drag
    private Vector2 originalItemAnchoredPosition; // Local slot position for snap-back
    private static ItemSlot draggingSlot;
    void Awake()
    {
        rootCanvas = GetComponentInParent<Canvas>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        UpdateDisplay();
    }
    // ทำงานทุก frame
    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleSelected();
    }
    public void UpdateDisplay()
    {
        // if (hasItem)
        // {
        //     itemImage.gameObject.SetActive(true);
        // }
        // else
        // {
        //     itemImage.gameObject.SetActive(false);
        // }

        itemImage.gameObject.SetActive(hasItem);

        // if (isSelected)
        // {
        //     selectedGraphic.gameObject.SetActive(true);
        // }
        // else
        // {
        //     selectedGraphic.gameObject.SetActive(false);
        // }
        selectedGraphic.gameObject.SetActive(isSelected);
    }

    #region Add/Remove Item
    public void AddItem(Item item)
    {
        this.item = item;
        this.itemImage.sprite = item.itemIcon;
        hasItem = true;
        UpdateDisplay();
    }
    public Item RemoveItem()
    {
        Item tmpItem = this.item;
        this.item = null;
        hasItem = false;
        UpdateDisplay();
        return tmpItem;
        // TODO: โยงเชื่อม กับระบบ drop item
    }


    #endregion

    #region Selected
    public void ToggleSelected()
    {
        if (isSelected) Deselected();
        else Selected();
    }
    public void Selected()
    {
        // Debug.Log("Selectd");
        inventoryManager.DeselectedAllSlot();
        isSelected = true;
        UpdateDisplay();
    }
    public void Deselected()
    {
        isSelected = false;
        UpdateDisplay();
    }
    #endregion

    #region Drag and Drop
    public void OnDrag(PointerEventData eventData)
    {
        //ข้อกำหนดตอนลาก game design
        if (draggingSlot != this) return;
        if (itemImage == null) return;
        if (rootCanvas == null) return;

        RectTransform canvasRect = rootCanvas.transform as RectTransform;
        if (canvasRect == null)
        {
            return;
        }

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint))
        {
            // ทำให้รูปภาพ ตรงกับ mouse ที่ชี้
            itemImage.rectTransform.localPosition = localPoint;
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //ข้อกำหนดก่อนที่จะลาก gameDesign ล้วน
        if (!hasItem) return;
        if (itemImage == null) return;
        if (!isSelected) return;
        // เริ่มลาก : เก็ลค่าที่ต้องใช้เพื่อ backup data หลังลาก
        Debug.Log("Begin Drag");
        draggingSlot = this;
        originalItemParent = itemImage.transform.parent;
        originalItemAnchoredPosition = itemImage.rectTransform.anchoredPosition;
        itemImage.raycastTarget = false;

        if (rootCanvas != null)
        {
            itemImage.transform.SetParent(rootCanvas.transform, true);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (draggingSlot == this) return;
        if (draggingSlot == null) return;
        if (!draggingSlot.hasItem) return;

        // 1. ถ้าตัวเราว่างเปล่า (ไม่มีไอเท็ม)
        if (!hasItem)
        {
            AddItem(draggingSlot.RemoveItem());
            Selected();
            return;
        }
        // 2. ถ้้าตัวเรามีไอเท็มอยู่แล้ว (สลับไอเท็ม)
        Item tempItem = item;
        AddItem(draggingSlot.item);
        draggingSlot.AddItem(tempItem);
        Selected();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingSlot != this) return;
        if (itemImage == null) return;

        Debug.Log("EndDrag");
        itemImage.transform.SetParent(originalItemParent, true);
        itemImage.rectTransform.anchoredPosition = originalItemAnchoredPosition;
        itemImage.raycastTarget = true;
        draggingSlot = null;
    }
    #endregion
}
