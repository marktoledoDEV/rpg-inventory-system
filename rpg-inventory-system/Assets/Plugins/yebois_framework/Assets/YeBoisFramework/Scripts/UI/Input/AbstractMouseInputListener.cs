using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YeBoisFramework.BoisMessaging;

namespace YeBoisFramework.UI
{
    public abstract class AbstractMouseInputListener : AbstractMonoBoisComponent
    {
        [SerializeField] protected BoisUIMouseHandler mMouseHandler;

        protected override void Awake() {
            base.Awake();

            mMouseHandler.AddMouseListener(this);
        }
    }
}
