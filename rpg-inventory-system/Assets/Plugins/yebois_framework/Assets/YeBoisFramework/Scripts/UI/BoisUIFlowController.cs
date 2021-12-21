using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YeBoisFramework.BoisMessaging;

namespace YeBoisFramework.UI
{
    [RequireComponent(typeof(Animator))]
    public class BoisUIFlowController : AbstractMonoBoisComponent
    {
        public static string UI_STATE_CHANGE_MSG = "ui_state_change"; //[TODO] we need to make a centralized Bois Message registry

        private Animator mFlowController;
        
        private void Start()
        {
            mFlowController = GetComponent<Animator>();
            CacheMethod(UI_STATE_CHANGE_MSG, TriggerStateChange);
        }

        private void TriggerStateChange(object o)
        {
            string trigger = o as string;
            if(trigger == null){
                Debug.LogError("TriggerStateChange, parameter was not the correct type", this);
                return;
            }

            mFlowController.SetTrigger(trigger);
        }
    }
}
