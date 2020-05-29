using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject gameController;
    [SerializeField] GameObject player;
    [SerializeField] GameObject dialogueBox;
    Text dialogueToUI;
    [SerializeField] int dialogueNumber;
    [SerializeField] string dialogueText1;
    [SerializeField] string dialogueText2;
    [SerializeField] string dialogueText3;
    [SerializeField] string dialogueText4;

    PlayerController playerController;
    [SerializeField] bool AcceptPressed;
    [SerializeField] bool endGhost = false;
    int textAdvance = 0;

    bool advanceStop;

    

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        dialogueToUI = dialogueBox.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract()
    {
        Debug.Log("The ghost is talking");
        Dialogue();
    }

    public void Dialogue()
    {
        gameController.GetComponent<GameController>().isMessage = true;
        gameController.GetComponent<GameController>().activeMessage = gameObject;

        AcceptPressed = playerController.AcceptPressed;
        advanceStop = false;
        
        if (textAdvance < 1)
        {

            dialogueToUI.text = dialogueText1;
            dialogueBox.SetActive(true);
            textAdvance = 1;
            advanceStop = true;
        }

        if (AcceptPressed)
        {
            Debug.Log(textAdvance);
            if ((dialogueNumber > 1) && (textAdvance == 1) && (advanceStop == false))
            {
                dialogueToUI.text = dialogueText2;
                textAdvance = 2;
                advanceStop = true;
            }
            else if ((dialogueNumber > 2) && (textAdvance == 2) && (advanceStop == false))
            {
                dialogueToUI.text = dialogueText3;
                textAdvance = 3;
                advanceStop = true;
            }
            else if ((dialogueNumber > 3) && (textAdvance == 3) && (advanceStop == false))
            {
                dialogueToUI.text = dialogueText4;
                textAdvance = 4;
                advanceStop = true;
            }
            else if ((dialogueNumber == textAdvance) && (advanceStop == false))
            {
                EndDialogue();
                advanceStop = true;
            }

        }

    }

    public void EndDialogue()
    {
        Debug.Log("end");
        dialogueBox.SetActive(false);
        gameController.GetComponent<GameController>().isMessage = false;
        gameController.GetComponent<GameController>().activeMessage = null;
        textAdvance = 0;

        if (endGhost == true)
        {
            Application.Quit();
        }
    }

}
