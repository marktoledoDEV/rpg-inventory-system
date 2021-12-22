using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeBoisFramework.UI
{
    //ties a bois message and keycode together
    [Serializable]
    public struct BoisMessageKeyBinding
    {
        public string mBoisMessage;
        public KeyCode mKeyCode;
        public bool mIsPressDown;
    }
}
