using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YeBoisFramework.BoisMessaging;

public class InventorySlotImage : AbstractMonoBoisComponent
{
    [SerializeField] private Image mImage;

    [Header("Bois Messagenger Properties")]
    [SerializeField] private string mItemAddedMsg = "";
    [SerializeField] private string mItemRemovedMsg = "";
    [SerializeField] private string mItemSlotDisabled = "";

    [Header("Properties")]
    [SerializeField] private Color mDisabledColor;

    protected override void Awake() {
        base.Awake();

        if(mImage == null) {
            mImage = GetComponent<Image>();
        }
        applyImage(null, true);
        CacheMethod(mItemAddedMsg, onItemAdded);
        CacheMethod(mItemRemovedMsg, onItemRemoved);
        CacheMethod(mItemSlotDisabled, onItemDisabled);
    }

    private void onItemAdded(object o) {
        Sprite sprite = o as Sprite;
        if(sprite != null) {
            applyImage(sprite, false);
        }
    }

    private void onItemRemoved(object o) {
        applyImage(null, true);
    }

    private void onItemDisabled(object o) {
        mImage.color = mDisabledColor;
    }

    private void applyImage(Sprite sprite, bool disabled) {
        mImage.sprite = sprite;
        Color color = mImage.color;
        color.a = disabled ? 0 : 255;
        mImage.color = color;
    }
}
