using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject dialogueBox;
    public GameObject activeMessage;
    public bool isMessage = false;
    public bool advanceStop;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMessage)
        {
            player.GetComponent<PlayerController>().Freeze();

            //Debug.Log("If you can read this message, you're not reading the other message.");
        }
        else if (!isMessage)
        {
            player.GetComponent<PlayerController>().Unfreeze();
        }

        if (activeMessage  != null)
        {
            Message();
        }

        if (Input.GetAxisRaw("Accept") != 0)
        {
            advanceStop = true;
        }
        else if (Input.GetAxisRaw("Accept") == 0)
        {
            advanceStop = false;
        }
    }

    public void Message()
    {
        if (activeMessage.tag == "Ghost")
        {
            activeMessage.GetComponent<Ghost>().Dialogue();
        }
        
    }

}
