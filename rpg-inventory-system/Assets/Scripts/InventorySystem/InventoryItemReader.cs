using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YeBoisFramework.BoisMessaging;
using YeBoisFramework.UI;

public class InventoryItemReader : AbstractMouseInputListener
{
    [Header("Bois Messages")]
    [SerializeField] private string mItemHovered;
    [SerializeField] private string mItemCleared;
    [SerializeField] private string mMousePressUpMsg;
    [SerializeField] private string mMousePressDownMsg;
    [SerializeField] private List<AbstractMonoBoisComponent> mListeners = new List<AbstractMonoBoisComponent>();

    [Header("Raycasting Data")]
    [SerializeField] private GraphicRaycaster mRaycaster;
    [SerializeField] private EventSystem mEventSystem;
    [SerializeField] private RectTransform mCanvasRect;
    private PointerEventData mPointerEventData;

    //Internal Properties
    private bool mStopReading = false;

    protected override void Awake() {
        base.Awake();
        mEventSystem = FindObjectOfType<EventSystem>();

        CacheMethod(mMousePressDownMsg, onItemSelected);
        CacheMethod(mMousePressUpMsg, onItemDeselected);
    }

    // Update is called once per frame
    private void Update()
    {
        if(mStopReading) {
            return;
        }
        //Set up the new Pointer Event
        mPointerEventData = new PointerEventData(mEventSystem);
        //Set the Pointer Event Position to that of the game object
        mPointerEventData.position = transform.position;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        mRaycaster.Raycast(mPointerEventData, results);

        if(results.Count > 0) {
            foreach(RaycastResult result in results) {
                if(result.gameObject.CompareTag("mouseDetectable")){
                    InventoryGridSlot slot = result.gameObject.GetComponent<InventoryGridSlot>();
                    if(slot != null && slot.isSlotOccupied) {
                        callBoisMessageToListeners(mItemHovered, slot.InventoryItemData);
                    }
                }

            }
        }
    }

    private void onItemSelected(object o) {
        mStopReading = true;
    }

    private void onItemDeselected(object o) {
        mStopReading = false;
        //callBoisMessageToListeners(mItemCleared);
    }

    private void callBoisMessageToListeners(string msg, ItemData item = null) {
        foreach(AbstractMonoBoisComponent listener in mListeners) {
            Call(msg, listener.gameObject, item);
        }
    }
}
