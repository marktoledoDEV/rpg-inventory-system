using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YeBoisFramework.BoisMessaging;

public class InventoryGridSlot : AbstractMonoBoisComponent {
    [SerializeField]
    private ItemData mData;
    public ItemData InventoryItemData {
        get { return mData; }
    }

    [SerializeField] private GameObject mItemImage;
    [SerializeField] private string mItemAddedMsg = "";
    [SerializeField] private string mItemRemovedMsg= "";
    [SerializeField] private string mItemSlotDisabled = "";

    //GridSystem Dependencies
    private string mOnItemChangedMessage;
    private InventoryGridSystem mInventoryGrid;

    public bool isSlotOccupied {
        get { return mData != null; }
    }

    public void Initialize(string name, InventoryGridSystem inventory, string boisMessage) {
        gameObject.name = name;
        mInventoryGrid = inventory;
        mOnItemChangedMessage = boisMessage;
    }

    public void AddItemToSlotWithoutSaving(ItemData item) {
        mData = item;
        Call(mItemAddedMsg, mItemImage, mData.ItemImage);
    }

    public void AddItemToSlot(ItemData item){
        AddItemToSlotWithoutSaving(item);
        Call(mOnItemChangedMessage, mInventoryGrid.gameObject);
    }

    public ItemData RemoveItemFromSlot() {
        ItemData data = mData;
        Call(mItemRemovedMsg, mItemImage);
        Call(mOnItemChangedMessage, mInventoryGrid.gameObject);
        mData = null;
        return data;
    }

    protected override void Awake() {
        base.Awake();

        CacheMethod(mItemSlotDisabled, onItemSlotDisabled);
    }

    private void onItemSlotDisabled(object o) {
        Call(mItemSlotDisabled, mItemImage);
    }
}
