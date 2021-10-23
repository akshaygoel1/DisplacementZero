using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Item itemType;
    Vector3 lastPos = Vector3.zero;


    private void OnMouseDown()
    {
        lastPos = transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
    }

    private void OnMouseUp()
    {
        transform.position = lastPos;
    }

}
