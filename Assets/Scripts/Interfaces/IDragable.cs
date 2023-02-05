using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable 
{
    public void StartDrag(Transform refPos);
    public void EndDrag(Transform target);
    public void ActionDrop(Card _card);
    public void OnDropAble();
    public void OnDropNotAble();
}
