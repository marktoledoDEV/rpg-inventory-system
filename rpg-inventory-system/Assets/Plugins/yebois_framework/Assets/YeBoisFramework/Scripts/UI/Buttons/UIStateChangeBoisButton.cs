using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YeBoisFramework.BoisMessaging;

namespace YeBoisFramework.UI
{
    public class UIStateChangeBoisButton : BoisButton
    {
        [SerializeField]
        private string mTriggerMsg = "";

        private BoisUIFlowController mFlowController;

        protected override void Start()
        {
            base.Start();
            mFlowController = GetComponentInParent<BoisUIFlowController>();
            if(mFlowController == null) 
            {
                Debug.LogError("mFlowController is Null!", this);
            }
        }

        protected override void onButtonClick() 
        {
            messenger.Call(BoisUIFlowController.UI_STATE_CHANGE_MSG, mFlowController, mTriggerMsg);
        } 
    }
}
