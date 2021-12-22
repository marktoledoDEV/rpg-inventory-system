using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGridSlot : MonoBehaviour {
    [SerializeField]
    private ItemData mData;
    public ItemData InventoryItemData {
        get { return mData; }
        set { mData = value; }
    }

    public bool isSlotOccupied {
        get { return mData != null; }
    }
}
