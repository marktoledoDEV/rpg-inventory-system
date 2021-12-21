using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeBoisFramework.BoisMessaging
{
    public abstract class AbstractMonoBoisComponent : MonoBehaviour, IBoisListener
    {
        protected BoisMessenger messenger;

        protected virtual void Awake()
        {
            messenger = new BoisMessenger(this);
        }

        public void CacheMethod(string msg, BoisMessenger.BoisMethod method)
        {
            messenger.CacheMethod(msg, method);
        }

        public void Call(string msg, GameObject targetGO, object parameters = null)
        {
            messenger.Call(msg, targetGO, parameters);
        }

        public void Call(string msg, IBoisListener listener, object parameters = null)
        {
            messenger.Call(msg, listener, parameters);
        }

        public void Broadcast(string msg, object parameters = null)
        {
            messenger.Broadcast(msg, parameters);
        }
    }
}
