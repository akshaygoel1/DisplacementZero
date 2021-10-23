using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float speed = 5f;
    public float runSpeed = 10f;
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position = new Vector3(player.transform.position.x + speed*Time.deltaTime, player.transform.position.y, player.transform.position.z);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.position = new Vector3(player.transform.position.x - speed * Time.deltaTime, player.transform.position.y, player.transform.position.z);

        }
    }
}
