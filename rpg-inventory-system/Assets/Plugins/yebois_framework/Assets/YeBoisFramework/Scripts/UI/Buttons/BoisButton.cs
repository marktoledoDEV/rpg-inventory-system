using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YeBoisFramework.BoisMessaging;

namespace YeBoisFramework.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class BoisButton : AbstractMonoBoisComponent
    {

        private Button mButton;

        protected virtual void Start()
        {
            mButton = GetComponent<Button>();
            mButton.onClick.AddListener(onButtonClick);
        }

        protected virtual void onButtonClick() 
        {
            Debug.Log("Bois Button Message", this);
        }
    }
}
