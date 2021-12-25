using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YeBoisFramework.BoisMessaging;

public class InventoryDetailContainer : AbstractMonoBoisComponent
{
    [Header("Bois Messagenger Properties")]
    [SerializeField] private string mItemHoveredMsg = "";
    [SerializeField] private string mItemDeselectedMsg = "";

    [Header("UI Elements")]
    [SerializeField] InventorySlotImage mSelectedItemImage;
    [SerializeField] InventorySlotText mSelectedItemTitle;
    [SerializeField] InventorySlotText mSelectedItemDescription;

    protected override void Awake() {
        base.Awake();

        CacheMethod(mItemHoveredMsg, onItemHovered);
    }

    private void onItemHovered(object o) {
        ItemData data = o as ItemData;
        if(data == null) {
            return;
        }
        Call(mItemHoveredMsg, mSelectedItemImage.gameObject, data.ItemImage);
        Call(mItemHoveredMsg, mSelectedItemTitle.gameObject, data.ItemName);
        Call(mItemHoveredMsg, mSelectedItemDescription.gameObject, data.ItemDescription);
    }
}
