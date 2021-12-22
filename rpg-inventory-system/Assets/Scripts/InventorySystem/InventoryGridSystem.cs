using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGridSystem : MonoBehaviour
{
    private List<List<InventoryGridSlot>> _gridData;
    public List<List<InventoryGridSlot>> GridData {
        get { return _gridData; }
    }

    [Header("Default Data")]
    [SerializeField] private InventoryGridSlot mDefaultSlotPrefab;

    [SerializeField] private int mMaxRows = 10;
    [SerializeField] private int mMaxColumn = 9;

    private void Awake() {
        setupGrid();
    }

    private void setupGrid() {
        for(int row = 0; row < mMaxRows; row++) {
            for(int column = 0; column < mMaxColumn; column++) {
                GameObject slot = Instantiate(mDefaultSlotPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform).gameObject;
                slot.name = "ItemSlotRow" + row + "Column" + column;
            }
        }
    }
}
