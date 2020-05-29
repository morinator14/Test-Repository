using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class PlayerController : MonoBehaviour
{
    //public delegate void PlayerHandler();
    //public event PlayerHandler OnDoorOpen;
    
    Rigidbody2D body;

    float horizontalSpeed;
    float verticalSpeed;

    bool IsMovingHorizontally
    {
        get { return horizontalSpeed < 0 || horizontalSpeed > 0; }
    }

    bool IsMovingVertically
    {
        get { return verticalSpeed < 0 || verticalSpeed > 0; }
    }

    bool IsMoving
    {
        get { return IsMovingHorizontally || IsMovingVertically; }
    }

    bool IsRunning
    {
        get { return Input.GetButton(PlayerButtonActions.Run) && IsMoving;}
    }

    public bool AcceptPressed
    {
        get { return Input.GetButtonDown(PlayerButtonActions.Accept); }
    }

    float moveLimiter = 0.7f;

    [SerializeField] GameObject gameController;

    [SerializeField] Animator animator;
    public Animator Animator
    {
        get { return animator; }
        set { animator = value; }
    }

    [SerializeField] float walkSpeed = 2.0f;
    public float WalkSpeed
    {
        get { return walkSpeed; }
        set { walkSpeed = value; }
    }

    [SerializeField] float runSpeed = 5.0f;
    public float RunSpeed
    {
        get { return runSpeed; }
        set { runSpeed = value; }
    }

    float moveSpeed;

    [SerializeField] Flowchart dialogue;

    Vector2 moveDirection;

    [SerializeField] GameObject door;
    [SerializeField] GameObject ghost;
    [SerializeField] GameObject key;

    public bool frozen;

    

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        door = null;

        CameraFollowPlayer();

    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen)
        {
            CameraFollowPlayer();

            InputHandler();

            Interact();
        }
        else
        {
            body.velocity = new Vector2 (0, 0);
        }

        //if (AcceptPressed)
        //{
        //    Debug.Log ("Accept");
        //}


    }

    void FixedUpdate()
    {
        Movement();
    }

    void OnTriggerEnter2D (Collider2D otherCollider)
    {
        if (otherCollider.tag == "Door")
        {
            door = otherCollider.gameObject;
            //OpenDoor();
        }
        else
        {
            door = null;
        }

        if (otherCollider.tag == "Ghost")
        {
            ghost = otherCollider.gameObject;
        }
        else
        {
            ghost = null;
        }
    }

    void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (door != null)
        {
            CloseDoor();
        }
        if (ghost != null)
        {
            ghost = null;
        }
    }

    void InputHandler()
    {
        var isRunning = IsRunning;
        moveSpeed = isRunning ? RunSpeed : WalkSpeed;

        horizontalSpeed = Input.GetAxisRaw(PlayerAxis.Horizontal);
        verticalSpeed = Input.GetAxisRaw(PlayerAxis.Vertical);

        moveDirection = new Vector2(horizontalSpeed, verticalSpeed);

        if (isRunning)
        {
            Animator.SetBool(PlayerProperties.Run, true);
        }
    }

    void Movement()
    {
        if (IsMovingVertically && IsMovingHorizontally)
        {
            horizontalSpeed *= moveLimiter;
            verticalSpeed *= moveLimiter;
        }

        body.velocity = new Vector2(horizontalSpeed * moveSpeed, verticalSpeed * moveSpeed);
    }

    void Interact()
    {
        if (AcceptPressed)
        {
            if (ghost != null)
            {
                TalkGhost();
            }
            if (door != null)
            {
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        //if (AcceptPressed && door != null)
        //{
            GameObject closedDoor = door.GetComponentInChildren<ClosedDoor>().gameObject;

            Debug.Log("Open!");
            
            if (closedDoor.activeInHierarchy)
            {
                closedDoor.SetActive(false);
            }

        //}
    }

    void CloseDoor()
    {
        GameObject closedDoor = door.GetComponent<Door>().closedDoor;

        if (!closedDoor.activeInHierarchy)
        {
            Debug.Log("Close!");
        
            closedDoor.SetActive(true);
        
            door = null;
        }
    }

    void TalkGhost()
    {
        Debug.Log("Ghost conversation attempted");
        ghost.GetComponent<Ghost>().OnInteract();
    }

    void GetKey()
    {

    }

    public void Freeze()
    {
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        frozen = true;
    }

    public void Unfreeze()
    {
        body.constraints = RigidbodyConstraints2D.None;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        frozen = false;
    }


    void CameraFollowPlayer()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        cameraPos = new Vector3 (transform.position.x, transform.position.y, -10f);

        Camera.main.transform.position = cameraPos;
    }

    static class PlayerProperties
    {
        public static readonly int Run = 0;
    }


    public static class PlayerButtonActions
    {
        public static readonly string Accept = "Accept";
        public static readonly string Run = "Run";
    }

    static class PlayerAxis
    {
        public static readonly string Horizontal = "Horizontal";
        public static readonly string Vertical = "Vertical";
    }

}
