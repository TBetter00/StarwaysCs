using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    public Image itemImage;
    public GameObject selectedDisplay;
    [Header("Boolean Variable")]
    public bool hasItem;
    public bool isSelected;
    private Canvas rootCanvas; // Cached canvas for coordinate conversion and drag visuals
    private Transform originalItemParent; // Where the icon returns after drag
    private Vector2 originalItemAnchoredPosition; // Local slot position for snap-back
    private static ItemSlot draggingSlot; // Shared drag source so any slot can accept a drop
    // [Header("Reference")]
    // public InventoryManager inventoryManager;
    void Awake()
    {
        // if(!inventoryManager) inventoryManager = FindAnyObjectByType<InventoryManager>();
        rootCanvas = GetComponentInParent<Canvas>(); // Find the nearest parent canvas once
        RefreshDisplay();
    }
    #region Add/Remove
    public void AddItem(Item item)
    {
        Debug.Log("Add in slot");
        this.item = item;
        this.itemImage.sprite = item.itemSprite;
        hasItem = true;
        RefreshDisplay();
    }

    public Item RemoveItem()
    {
        Item item = this.item;
        this.item = null;
        hasItem = false;
        RefreshDisplay();
        return item;
    }
    #endregion

    /// <summary>
    /// set Display by mulitple boolean variable
    /// </summary>
    public void RefreshDisplay()
    {
        itemImage?.gameObject.SetActive(hasItem);
        selectedDisplay?.SetActive(isSelected);
    }
    #region Selected
    public void OnSelected()
    {
        InventoryManager.instance.DeselectedAllSlot();
        isSelected = true;
        RefreshDisplay();
    }
    public void OnDeselected()
    {
        isSelected = false;
        RefreshDisplay();
    }
    public void ToggleSelected()
    {
        if (isSelected) OnDeselected();
        else OnSelected();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleSelected();
    }
    #endregion

    #region Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!hasItem || itemImage == null || !isSelected)
        {
            return;
        }

        // Start a drag: record source slot and icon state
        draggingSlot = this;
        originalItemParent = itemImage.transform.parent;
        originalItemAnchoredPosition = itemImage.rectTransform.anchoredPosition;
        itemImage.raycastTarget = false;

        if (rootCanvas != null)
        {
            // Move icon under canvas so it can follow the cursor above all slots
            itemImage.transform.SetParent(rootCanvas.transform, true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingSlot != this || itemImage == null || rootCanvas == null)
        {
            return;
        }

        // Convert screen pointer to canvas local point and move the icon there
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
            itemImage.rectTransform.localPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingSlot != this || itemImage == null)
        {
            return;
        }

        // Snap icon back into its slot; OnDrop handles the data move/swap
        itemImage.transform.SetParent(originalItemParent, true);
        itemImage.rectTransform.anchoredPosition = originalItemAnchoredPosition;
        itemImage.raycastTarget = true;
        draggingSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (draggingSlot == null || draggingSlot == this || !draggingSlot.hasItem)
        {
            return;
        }

        // Empty target: move item into this slot
        if (!hasItem)
        {
            AddItem(draggingSlot.item);
            draggingSlot.RemoveItem();
            OnSelected();
            return;
        }

        // Occupied target: swap items
        Item tempItem = item;
        AddItem(draggingSlot.item);
        draggingSlot.AddItem(tempItem);
        OnSelected();
    }

    #endregion
}
