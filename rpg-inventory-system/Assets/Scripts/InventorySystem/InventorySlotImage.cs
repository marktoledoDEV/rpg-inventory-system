using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YeBoisFramework.BoisMessaging;

[RequireComponent(typeof(Image))]
public class InventorySlotImage : AbstractMonoBoisComponent
{
    [SerializeField] private Image mImage;
    [SerializeField] private Sprite  mDefaultImage;

    [SerializeField] private string mItemAddedMsg = "";
    [SerializeField] private string mItemRemovedMsg = "";

    protected override void Awake() {
        base.Awake();

        mImage = GetComponent<Image>();
        CacheMethod(mItemAddedMsg, onItemAdded);
        CacheMethod(mItemRemovedMsg, onItemRemoved);
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

    private void applyImage(Sprite sprite, bool disabled) {
        mImage.sprite = sprite;
        Color color = mImage.color;
        color.a = disabled ? 0 : 255;
        mImage.color = color;
    }
}
