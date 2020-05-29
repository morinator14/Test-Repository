using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] PlayerController player;

    bool AcceptPressed
    {
        get { return Input.GetButtonDown("Accept");}
    }

    bool playerPresent = false;

    public GameObject closedDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ///if (playerPresent)
        ///{
        ///    player.OnDoorOpen += OperateDoor;
        ///}

    }

    void OnTriggerEnter2D (Collider2D otherCollider)
    {
        if (otherCollider.tag == "Player")
        {
            playerPresent = true;
            //player.OnDoorOpen += OperateDoor;
        }
        else
        {
            playerPresent = false;
        }
    }

    void OperateDoor()
    {
        if (closedDoor.activeInHierarchy)
        {
            closedDoor.SetActive(false);
        }
        else if (!closedDoor.activeInHierarchy)
        {
            closedDoor.SetActive(true);
        }
    }

}
