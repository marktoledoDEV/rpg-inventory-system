using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YeBoisFramework.BoisMessaging;
using YeBoisFramework.SaveLoad;
using YeBoisFramework.UI;
using YeBoisFramework.Utility;

[RequireComponent(typeof(FlexibleGridLayout))]
public class InventoryGridSystem : AbstractMonoBoisComponent, ISaveLoadEntity
{
    private List<List<InventoryGridSlot>> _gridData = new List<List<InventoryGridSlot>>();
    public List<List<InventoryGridSlot>> GridData {
        get { return _gridData; }
    }

    [Header("Default Data")]
    [SerializeField] private InventoryGridSlot mDefaultSlotPrefab;

    [Header("Bois Message")]
    [SerializeField] private string mOnInventoryChangedMessage;

    //Dependencies
    private ItemRegistry mItemRegistry;
    private FlexibleGridLayout mGridLayout;
    
    //Properties are for testing
    private int rngRow = 0;
    private int rngColumn = 0;

    protected override void Awake() {
        base.Awake();
        
        mItemRegistry = ServiceLocator.Instance.GetService<ItemRegistry>();
        mGridLayout = GetComponent<FlexibleGridLayout>();

        CacheMethod(mOnInventoryChangedMessage, OnInventorySave);
        
        setupGrid();
        OnLoad();
    }

    private void Update() {
        DebugTestCode();
    }

    private void setupGrid() {
        for(int row = 0; row < mGridLayout.rows; row++) {
            List<InventoryGridSlot> rowSlots = new List<InventoryGridSlot>(); 
            for(int column = 0; column < mGridLayout.columns; column++) {
                InventoryGridSlot slot = Instantiate(mDefaultSlotPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                slot.Initialize("ItemSlotRow" + row + "Column" + column, this, mOnInventoryChangedMessage);
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
                rngRow = rnd.Next(mGridLayout.rows);
                rngColumn = rnd.Next(mGridLayout.columns);
                InventoryGridSlot slot = _gridData[rngRow][rngColumn];
                if(!slot.isSlotOccupied) {
                    slot.AddItemToSlot(data);
                    itemPlaced = true;
                }
            }
            Debug.Log("Item [" + data.ItemID + "] Added to Slot " + rngRow + "x" + rngColumn);
        }
    }

    //Bois Message
    private void OnInventorySave(object o) {
        OnSave();
    }

    //Save Load Entity Functions
    public string SaveLoadName() {
        return "inventory";
    }

    public void OnSave() {
        //turn the entire inventory into json
        SerializedInventory inventory = new SerializedInventory();
        inventory.inventory = new List<SerializedItemData>();
        for(int row = 0; row < mGridLayout.rows; row++) {
            for(int column = 0; column < mGridLayout.columns; column++) {
                InventoryGridSlot slot = _gridData[row][column];
                if(slot.isSlotOccupied) {
                    SerializedItemData item = new SerializedItemData();
                    item.item_id = slot.InventoryItemData.ItemID;
                    item.rowPosition = row;
                    item.columnPosition = column;
                    inventory.inventory.Add(item);
                }
            }
        }
        string output = JsonConvert.SerializeObject(inventory);

        //save output to json file
        SaveLoadService saveLoadService = ServiceLocator.Instance.GetService<SaveLoadService>();
        saveLoadService.OnSerialize(SaveLoadName(), SaveLoadService.FileType.JSON, output);
    }

    public void OnLoad() {
        SaveLoadService saveLoadService = ServiceLocator.Instance.GetService<SaveLoadService>();
        string output = saveLoadService.OnDeSerialize(SaveLoadName(), SaveLoadService.FileType.JSON);
        if(output == "") {
            return;
        }

        ItemRegistry itemRegistry = ServiceLocator.Instance.GetService<ItemRegistry>();
        SerializedInventory deserializedInventory = JsonConvert.DeserializeObject<SerializedInventory>(output);
        foreach(SerializedItemData item in deserializedInventory.inventory) {
            _gridData[item.rowPosition][item.columnPosition].AddItemToSlotWithoutSaving(itemRegistry.GetItem(item.item_id));
        }
    }
}
