using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YeBoisFramework.Utility;

public class InventoryGridSystem : MonoBehaviour
{
    private List<List<InventoryGridSlot>> _gridData = new List<List<InventoryGridSlot>>();
    public List<List<InventoryGridSlot>> GridData {
        get { return _gridData; }
    }

    [Header("Default Data")]
    [SerializeField] private InventoryGridSlot mDefaultSlotPrefab;

    [SerializeField] private int mMaxRows = 10;
    [SerializeField] private int mMaxColumn = 9;

    //Dependencies
    private ItemRegistry mItemRegistry;
    
    //Properties are for testing
    private int rngRow = 0;
    private int rngColumn = 0;

    private void Awake() {
        setupGrid();
        
        mItemRegistry = ServiceLocator.Instance.GetService<ItemRegistry>();
    }

    private void Update() {
        DebugTestCode();
    }

    private void setupGrid() {
        for(int row = 0; row < mMaxRows; row++) {
            List<InventoryGridSlot> rowSlots = new List<InventoryGridSlot>(); 
            for(int column = 0; column < mMaxColumn; column++) {
                InventoryGridSlot slot = Instantiate(mDefaultSlotPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                slot.gameObject.name = "ItemSlotRow" + row + "Column" + column;
                rowSlots.Add(slot);
            }
            _gridData.Add(rowSlots);
        }
    }

    private void DebugTestCode() {
        if(Input.GetKeyDown(KeyCode.A)){
            System.Random rnd = new System.Random();
            
            List<string> allItemIDs = mItemRegistry.GetItemIDs();
            int rngIndex = rnd.Next(allItemIDs.Count);
            ItemData data = mItemRegistry.GetItem(allItemIDs[rngIndex]);
            bool itemPlaced = false;
            while(!itemPlaced) {
                rngRow = rnd.Next(mMaxRows);
                rngColumn = rnd.Next(mMaxColumn);
                InventoryGridSlot slot = _gridData[rngRow][rngColumn];
                if(!slot.isSlotOccupied) {
                    slot.AddItemToSlot(data);
                    itemPlaced = true;
                }
            }
            Debug.Log("Item [" + data.ItemID + "] Added to Slot " + rngRow + "x" + rngColumn);
        }
    }
}
