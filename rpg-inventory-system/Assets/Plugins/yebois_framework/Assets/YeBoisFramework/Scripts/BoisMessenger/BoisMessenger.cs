using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeBoisFramework.BoisMessaging
{
    public class BoisMessenger
    {
        public delegate void BoisMethod(object o = null);

        private IBoisListener mCachedBoisListener;
        private Dictionary<string, BoisMethod> BoisMessageHandler = new Dictionary<string, BoisMethod>();

        public BoisMessenger(IBoisListener listener)
        {
            mCachedBoisListener = listener;
            BoisMessagerManager.Instance.AddBaseMessenger(mCachedBoisListener, this);
        }

        ~BoisMessenger()
        {
            BoisMessagerManager.Instance.RemoveBaseMessenger(mCachedBoisListener, this);
            mCachedBoisListener = null;
            BoisMessageHandler = null;
        }

        public void CacheMethod(string msg, BoisMethod action)
        {
            if (BoisMessageHandler.ContainsKey(msg))
            {
                BoisMessageHandler[msg] += action;
            }
            else
            {
                BoisMessageHandler.Add(msg, action);
            }
        }

        public void Call(string msg, IBoisListener listener, object parameters = null)
        {
            BoisMessagerManager.Instance.SendMessage(msg, listener, parameters);
        }

        public void Call(string msg, GameObject targetGO, object parameters = null)
        {
            BoisMessagerManager.Instance.SendMessage(msg, targetGO, parameters);
        }

        public void Broadcast(string msg, object parameters = null)
        {
            BoisMessagerManager.Instance.SendMessageToAll(msg, parameters);
        }

        public void ReceiveMessage(string msg, object parameters = null)
        {
            if (BoisMessageHandler.ContainsKey(msg))
            {
                if (BoisMessageHandler[msg] != null)
                {
                    BoisMessageHandler[msg](parameters);
                }
            }
        }
    }
}
