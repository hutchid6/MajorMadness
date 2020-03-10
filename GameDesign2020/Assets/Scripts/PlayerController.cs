using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Transform playerT;
    Animator playerAnim;
    float speed = 0.25f;
    float interactionRadius = 2.0f;
<<<<<<< HEAD
=======
    bool playerEnabled = true;
>>>>>>> parent of 9585aaa... Started Adding Test Enemy
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerT = GetComponent<Transform>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            playerT.Translate(new Vector3(1, 1, 0) * speed);
        }
<<<<<<< HEAD
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
=======

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckForNearbyNPC();
        }*/
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && FindObjectOfType<DialogueRunner>().isDialogueRunning != true)
>>>>>>> parent of 9585aaa... Started Adding Test Enemy
        {
            playerT.Translate(new Vector3(-1, 1, 0) * speed);
        }
        else if (Input.GetKey("s") && Input.GetKey("a"))
        {
            playerT.Translate(new Vector3(-1, -1, 0) * speed);
        }
        else if (Input.GetKey("s") && Input.GetKey("d"))
        {
            playerT.Translate(new Vector3(1, -1, 0) * speed);
        }
        else if(Input.GetKey("w"))
        {
            playerT.Translate(new Vector3(0, 1, 0) * speed);
        }
        else if (Input.GetKey("d"))
        {
            playerT.Translate(new Vector3(1, 0, 0) * speed);
            playerAnim.Play("playerWalkRight");
        }
        else if (Input.GetKey("s"))
        {
            playerT.Translate(new Vector3(0, -1, 0) * speed);
        }
        else if (Input.GetKey("a"))
        {
            playerT.Translate(new Vector3(-1, 0, 0) * speed);
        }
        else
        {
            playerAnim.Play("playerIdle");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckForNearbyNPC();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckForNearbyNPC();
        }
    }

    public void CheckForNearbyNPC()
    {
        var allParticipants = new List<Interactable>(FindObjectsOfType<Interactable>());
        var target = allParticipants.Find(delegate (Interactable p) {
            return string.IsNullOrEmpty(p.talkToNode) == false && // has a conversation node?
            (p.transform.position - transform.position).magnitude <= interactionRadius;//is in range?
        });
        if (target != null)
        {
            // Kick off the dialogue at this node.
            FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
        }
    }
}
