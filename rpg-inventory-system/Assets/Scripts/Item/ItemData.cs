using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "rpg-inventory-system/ItemData", order = 2)]
public class ItemData : ScriptableObject 
{
    [SerializeField]
    private Sprite _itemImage;
    public Sprite ItemImage 
    {
        get { return _itemImage; }
    }

    [SerializeField]
    private string _itemName;
    public string ItemName
    {
        get { return _itemName; }
        set { _itemName = value; }
    }

    [SerializeField]
    [TextArea]
    private string _itemDescription;
    public string ItemDescription
    {
        get { return _itemDescription; }
        set { _itemDescription = value; }
    }

    [SerializeField]
    private int _xSize;
    public int XSize
    {
        get { return _xSize; }
        set { _xSize = value; }
    }

    [SerializeField]
    private int _ySize;
    public int YSize
    {
        get { return _ySize; }
        set { _ySize = value; }
    }

}
