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

    public bool isSlotOccupied {
        get { return mData != null; }
    }

    public void AddItemToSlot(ItemData item){
        mData = item;
        Call(mItemAddedMsg, mItemImage, mData.ItemImage);
    }

    public ItemData RemoveItemFromSlot() {
        ItemData data = mData;
        Call(mItemRemovedMsg, mItemImage);
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
