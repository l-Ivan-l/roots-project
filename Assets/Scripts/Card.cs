using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Card : MonoBehaviour,IDragable
{
    [SerializeField] private int num = 0;
    [SerializeField] private TextMeshPro txt_Number;
    private Collider collider;
    Transform referencePos;
    private Vector3 originPos;

    private bool isOnDrag;

    [Header("DropStatus")]
    [SerializeField] Renderer renderer;
    [SerializeField] Material dropAbleMat;
    [SerializeField] Material dropNotAbleMat;
    Material originalMat;

    private void Start()
    {
        originalMat = renderer.material;
        originPos = transform.position;
        isOnDrag = false;
        collider = GetComponent<Collider>();
        CardEvents.Trigger(CardEventType.GetNumber, this);
    }

    void Update()
    {
        if(isOnDrag)
        {
            Vector3 newPos = new Vector3(referencePos.position.x, referencePos.position.y + 0.2f, referencePos.position.z);
            transform.position = newPos;
        }
    }

    public void SetNumber(int _num)
    {
        num = _num;
        txt_Number.text = _num.ToString();
    }

    public int GetNumber()
    {
        return num;
    }

    public void SumNumber(int newNum)
    {
        num += newNum;
        txt_Number.text = num.ToString();
    }
    public void StartDrag(Transform refPos)
    {
        collider.enabled = false;
        referencePos = refPos;
        isOnDrag = true;
    }

    public void EndDrag(Transform target)
    {
        isOnDrag = false;
        collider.enabled = true;
        if(target != null)
        {
            target.GetComponent<IDragable>().ActionDrop(this);
            CardEvents.Trigger(CardEventType.GetNumber, this);
        }
        renderer.material = originalMat;

        transform.position = originPos;

    }

    public void ActionDrop(Card _card)
    {
        SumNumber(_card.GetNumber());
    }

    public void OnDropAble()
    {
        renderer.material = dropAbleMat;
    }

    public void OnDropNotAble()
    {
        renderer.material = dropNotAbleMat;
    }
}
