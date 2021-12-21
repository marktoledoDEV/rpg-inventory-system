using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeBoisFramework.BoisMessaging
{
    public class BoisMessagerManager
    {
        //Singleton Initialization and Setup //[TODO] Make a ServceLocator
        private static BoisMessagerManager boisInstance = null;
        public static BoisMessagerManager Instance
        {
            get
            {
                if (boisInstance == null)
                {
                    boisInstance = new BoisMessagerManager();
                }
                return boisInstance;
            }
            private set { boisInstance = value; }
        }

        private BoisMessagerManager()
        {
            CachedBoisMessengers = new Dictionary<IBoisListener, List<BoisMessenger>>();
        }

        //Properties and Fields
        private Dictionary<IBoisListener, List<BoisMessenger>> CachedBoisMessengers;

        //Methods
        public void AddBaseMessenger(GameObject go, BoisMessenger bois)
        {
            if (go == null)
            {
                Debug.LogError("GAME OBJECT WAS NULL");
                return;
            }

            IBoisListener listener = go.GetComponent<IBoisListener>();
            if (listener == null)
            {
                Debug.LogError("listener not found on: " + go.name, go);
                return;
            }

            AddBaseMessenger(listener, bois);
        }

        public void AddBaseMessenger(IBoisListener listener, BoisMessenger bois)
        {
            if (!CachedBoisMessengers.ContainsKey(listener))
            {
                List<BoisMessenger> messengers = new List<BoisMessenger>();
                messengers.Add(bois);
                CachedBoisMessengers.Add(listener, messengers);
            }
            else
            {
                CachedBoisMessengers[listener].Add(bois);
            }
        }

        public void RemoveBaseMessenger(GameObject go, BoisMessenger bois)
        {
            if (go == null)
            {
                Debug.LogError("GAME OBJECT WAS NULL");
                return;
            }

            IBoisListener listener = go.GetComponent<IBoisListener>();
            if (listener == null)
            {
                Debug.LogError("listener not found on: " + go.name, go);
                return;
            }

            RemoveBaseMessenger(listener, bois);
        }
        public void RemoveBaseMessenger(IBoisListener listener, BoisMessenger bois)
        {
            if (CachedBoisMessengers.ContainsKey(listener))
            {
                if (CachedBoisMessengers[listener] != null
                || CachedBoisMessengers[listener].Count > 0)
                {
                    CachedBoisMessengers[listener].Remove(bois);
                }

                //if the list is empty remove the key and value from the dictionary
                if (CachedBoisMessengers[listener].Count == 0)
                {
                    CachedBoisMessengers.Remove(listener);
                }
            }
        }

        public void SendMessage(string msg, GameObject go, object parameters = null)
        {
            if (go == null)
            {
                Debug.LogError("Target Game Object is Null!");
                return;
            }

            IBoisListener listener = go.GetComponent<IBoisListener>();
            if (listener == null)
            {
                Debug.LogError("listener not found on: " + go.name, go);
                return;
            }

            SendMessage(msg, listener, parameters);
        }
        public void SendMessage(string msg, IBoisListener listener, object parameters = null)
        {
            if (CachedBoisMessengers.ContainsKey(listener))
            {
                BoisMessenger[] messengers = CachedBoisMessengers[listener].ToArray();
                for (int i = 0; i < messengers.Length; i++)
                {
                    messengers[i].ReceiveMessage(msg, parameters);
                }
            }
        }

        public void SendMessageToAll(string msg, object parameters = null)
        {
            foreach (KeyValuePair<IBoisListener, List<BoisMessenger>> entry in CachedBoisMessengers)
            {
                BoisMessenger[] messengers = entry.Value.ToArray();
                if (messengers != null && messengers.Length > 0)
                {
                    for (int i = 0; i < messengers.Length; i++)
                    {
                        messengers[i].ReceiveMessage(msg, parameters);
                    }
                }
            }
        }
    }
}
