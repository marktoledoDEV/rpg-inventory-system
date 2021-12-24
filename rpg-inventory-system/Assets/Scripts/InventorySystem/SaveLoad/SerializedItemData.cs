using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SerializedItemData
{
    public string item_id;
    public int rowPosition;
    public int columnPosition;
}

[Serializable]
public struct SerializedInventory
{
    public List<SerializedItemData> inventory;
}
