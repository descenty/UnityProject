using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSocket : MonoBehaviour
{
    private GameObject socketOutline;
    private GameObject itemOutline;
    private BoxCollider collider;
    public Pickable item;
    private bool _canPickItem = true;
    private Outliner outliner;

    public bool canPickItem
    {
        get => _canPickItem;
        set
        {
            _canPickItem = value;
            if (_canPickItem)
                item.canPick = true;
            else
                item.canPick = false;
        }
    }
    private void Awake()
    {
        socketOutline = transform.GetChild(0).gameObject;
        itemOutline = transform.GetChild(1).gameObject;
        outliner = gameObject.AddComponent<Outliner>();
        outliner.uniqueOutline = socketOutline;
        collider = GetComponent<BoxCollider>();

    }
    public void PlaceItem(Pickable item)
    {
        this.item = item;
        item.canPick = false;
        item.needRigid = false;
        item.Unpick(transform.position, transform);
        outliner.SetOutlineState(false);
        outliner.uniqueOutline = null;
        outliner.externalOutliner = item.outliner;
        outliner.SetOutlineState(true);
    }

    public Pickable PickItem()
    {
        Pickable outputItem = item;
        item.canPick = true;
        item.needRigid = true;
        outliner.SetOutlineState(false);
        outliner.uniqueOutline = socketOutline;
        outliner.SetOutlineState(true);
        item = null;
        return outputItem;
    }
}
