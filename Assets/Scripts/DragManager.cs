using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    private bool isDragging;
    private bool castingRay;
    [SerializeField] private Transform referencePosition;
    [SerializeField] private Transform target;
    [SerializeField] private Transform obj_OnDrag;
    [SerializeField] private LayerMask layerDragable;
    [SerializeField] private LayerMask layerPosition;
    Camera cam;
    Vector3 screenPos;
    Vector3 offset;
    RaycastHit hit;
    Ray ray;

    private void Start()
    {
        isDragging = false;
        castingRay = false;
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (target == null) return;

            target.gameObject.GetComponent<IDragable>().StartDrag(referencePosition);
            obj_OnDrag = target;
            if(target == obj_OnDrag)
                target = null;
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(obj_OnDrag != null)
            {
                obj_OnDrag.GetComponent<IDragable>().EndDrag(target);
                obj_OnDrag = null;
            }
        }
    }

    private void FixedUpdate()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray.origin,ray.direction, out hit, layerDragable))
        {
            if(hit.collider.gameObject.GetComponent<IDragable>() != null)
            {
                target = hit.collider.gameObject.transform;
                if(obj_OnDrag != null)
                {
                    obj_OnDrag.GetComponent<IDragable>().OnDropAble();
                }
            }
            else
            {
                if (obj_OnDrag != null)
                    obj_OnDrag.GetComponent<IDragable>().OnDropNotAble();
                target = null;
            }
            
        }
        else
        {
            target = null;
        }

        //Raycas position
        RaycastHit hitPos;
        Ray rayPos;

        rayPos = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayPos.origin, rayPos.direction, out hitPos, layerPosition))
        {
            referencePosition.position = hitPos.point;
        }

    }
}
