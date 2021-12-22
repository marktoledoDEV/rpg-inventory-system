using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YeBoisFramework.BoisMessaging;

namespace YeBoisFramework.UI
{
    public class BoisUIMouseHandler : AbstractMonoBoisComponent
    {
        [SerializeField] private List<BoisMessageKeyBinding> mBoisMsgKeyCodeBindings = new List<BoisMessageKeyBinding>();
        [SerializeField] private List<AbstractMouseInputListener> mBoisMouseListeners = new List<AbstractMouseInputListener>();

        [Header("Raycasting Data")]
        [SerializeField] private GraphicRaycaster mRaycaster;
        [SerializeField] private EventSystem mEventSystem;
        [SerializeField] private RectTransform canvasRect;
        private PointerEventData mPointerEventData;

        protected override void Awake() {
            base.Awake();
            
            mEventSystem = FindObjectOfType<EventSystem>();
        }

        private void Update() {
            updateMousePosition();
            checkBoisInputs();
        }

        public void AddMouseListener(AbstractMouseInputListener listener) {
            mBoisMouseListeners.Add(listener);
        }

        private GameObject fireRayCast() {
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
                        //Debug.Log("Hit " + result.gameObject.name); //uncomment for debugging purposes
                        return result.gameObject;
                    }

                }
            }
            //Debug.Log("Mouse RayCast did not hit anything", this); //uncomment for debugging purposes
            return null;
        }

        private void updateMousePosition() {
            Vector3 mousePosition = Input.mousePosition;
            transform.position = mousePosition;
        }

        private void checkBoisInputs() {
            foreach(BoisMessageKeyBinding binding in mBoisMsgKeyCodeBindings) {
                if(binding.mIsPressDown){
                    if(Input.GetKeyDown(binding.mKeyCode)) {
                        
                        callBoisMessageToListeners(binding.mBoisMessage);
                    }
                }
                else {
                    if(Input.GetKeyUp(binding.mKeyCode)) {
                        callBoisMessageToListeners(binding.mBoisMessage);
                    }
                }
            }
        }

        private void callBoisMessageToListeners(string msg) {
            GameObject hitObject = fireRayCast();
            foreach(AbstractMouseInputListener listener in mBoisMouseListeners) {
                Call(msg, listener.gameObject, hitObject);
            }
        }
    }
}
