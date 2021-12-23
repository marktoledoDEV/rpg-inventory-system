using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    [Header("Test")]
    [SerializeField] private ItemData mTestData;
    private int rngRow = 0;
    private int rngColumn = 0;

    private void Awake() {
        setupGrid();
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
            rngRow = rnd.Next(mMaxRows);
            rngColumn = rnd.Next(mMaxColumn);
            _gridData[rngRow][rngColumn].AddItemToSlot(mTestData);
            Debug.Log("Item Added to Slot " + rngRow + "x" + rngColumn);
        }

        if(Input.GetKeyDown(KeyCode.R)) {
            if(_gridData[rngRow][rngColumn].isSlotOccupied) {
                _gridData[rngRow][rngColumn].RemoveItemFromSlot();
                Debug.Log("Item Removed to Slot " + rngRow + "x" + rngColumn);
            }
        }
    }
}
