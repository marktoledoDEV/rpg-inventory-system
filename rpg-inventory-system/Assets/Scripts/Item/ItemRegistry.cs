using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YeBoisFramework.Utility;

public class ItemRegistry : MonoBehaviour, IService
{
    [SerializeField] private List<ItemData> mAllItemList = new List<ItemData>();
    private Dictionary<string, ItemData> mItemRegistry = new Dictionary<string, ItemData>();
    private List<string> mItemIdList = new List<string>();

    private void Awake() {
        //initializing mItemRegistry here
        foreach(ItemData item in mAllItemList) {
            mItemRegistry.Add(item.ItemID, item);
            mItemIdList.Add(item.ItemID);
        }

        //add itself to the ServiceLocator
        ServiceLocator.Instance.AddService(this);
    }

    public ItemData GetItem(string id) {
        ItemData data = null;
        mItemRegistry.TryGetValue(id, out data);

        if(data == null) {
            Debug.LogWarning("Item Registry does not contain ItemData w/ ID: " + id);
        }
        return data;
    }

    public List<string> GetItemIDs() {
        return mItemIdList;
    }
}
