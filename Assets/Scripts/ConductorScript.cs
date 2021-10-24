using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductorScript : MonoBehaviour
{
    public float speed = 3f;
    public Transform start, end;
    int dir = -1;
    // Start is called before the first frame update
    void Start()
    {
        MoveLeft();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, end.transform.position) < 0.1f)
        {
            MoveLeft();
            dir = -1;
        }
        else if (Vector3.Distance(this.transform.position, start.transform.position) < 0.1f)
        {
            MoveRight();
            dir = 1;
        }

        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * dir, transform.position.y, transform.position.z);
    }


    void MoveLeft()
    {
        transform.localScale = new Vector3(-0.2f, transform.localScale.y, transform.localScale.z);
    }


    void MoveRight()
    {
        transform.localScale = new Vector3(0.2f, transform.localScale.y, transform.localScale.z);
    }
}
