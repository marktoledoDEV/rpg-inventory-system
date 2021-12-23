using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YeBoisFramework.BoisMessaging;
using YeBoisFramework.UI;

public class InventoryItemHolder : AbstractMouseInputListener
{
    [Header("Bois Messages")]
    [SerializeField] private string mItemSelectedMsg;
    [SerializeField] private string mItemDeselectedMsg;
    [SerializeField] private string mMousePressUpMsg;
    [SerializeField] private string mMousePressDownMsg;
    [SerializeField] private List<AbstractMonoBoisComponent> mListeners = new List<AbstractMonoBoisComponent>();

    private InventoryGridSlot mSelectedInventorySlot;

    protected override void Awake() {
        base.Awake();

        CacheMethod(mMousePressDownMsg, onItemSelected);
        CacheMethod(mMousePressUpMsg, onItemDeselected);
    }

    private void onItemSelected(object o) {
        GameObject go = o as GameObject;
        if(go == null) {
            return;
        }

        InventoryGridSlot itemSlot = go.GetComponent<InventoryGridSlot>();
        if(itemSlot.isSlotOccupied) {
            mSelectedInventorySlot = itemSlot;
            CallToListeners(mItemSelectedMsg, mSelectedInventorySlot.InventoryItemData.ItemImage);
        }
    }

    private void onItemDeselected(object o) {
        GameObject go = o as GameObject;
        if(go == null) {
            return;
        }
        //[TODO] make the item swap here
        InventoryGridSlot itemSlot = go.GetComponent<InventoryGridSlot>();
        if(!itemSlot.isSlotOccupied) {
            ItemData itemData = mSelectedInventorySlot.RemoveItemFromSlot();
            itemSlot.AddItemToSlot(itemData);
        }
        
        mSelectedInventorySlot = null;
        CallToListeners(mItemDeselectedMsg);
    }

    private void CallToListeners(string message, object parameter = null) {
        foreach(AbstractMonoBoisComponent listener in mListeners) {
            Call(message, listener, parameter);
        }
    }
}
