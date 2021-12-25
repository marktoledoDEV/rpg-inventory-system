using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YeBoisFramework.BoisMessaging;

public class InventorySlotText : AbstractMonoBoisComponent
{
    [SerializeField] private Text mText;

    [Header("Bois Messagenger Properties")]
    [SerializeField] private string mItemAddedMsg = "";
    [SerializeField] private string mItemRemovedMsg = "";

    [Header("Properties")]
    [SerializeField] private string mDisabledText = "";

    protected override void Awake() {
        base.Awake();
        if(mText == null) {
            mText = GetComponent<Text>();
        }
        mText.text = mDisabledText;

        CacheMethod(mItemAddedMsg, onItemAdded);
        CacheMethod(mItemRemovedMsg, onItemRemoved);
    }

    private void onItemAdded(object o) {
        string text = o as string;
        if(text != null) {
            mText.text = text;
        }
    }

    private void onItemRemoved(object o) {
        mText.text = mDisabledText;
    }
}
