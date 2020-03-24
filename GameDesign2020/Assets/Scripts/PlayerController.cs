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
    SpriteRenderer playerSprite;
    float speed = 0.15f;
    float interactionRadius = 2.0f;
    bool playerEnabled = true;

    enum PlayerDirection
    {
        up,
        upRight,
        right,
        downRight,
        down,
        downLeft,
        left,
        upLeft
    }
    PlayerDirection tagDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerT = GetComponent<Transform>();
        playerAnim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerEnabled)
        {
            if (Input.GetKey("w") && Input.GetKey("a"))
            {
                playerT.Translate(new Vector3(-0.75f, 0.75f, 0) * speed);
                tagDirection = PlayerDirection.upLeft;
            }
            else if(Input.GetKey("w") && Input.GetKey("d"))
            {
                playerT.Translate(new Vector3(0.75f, 0.75f, 0) * speed);
                tagDirection = PlayerDirection.upRight;
            }
            else if (Input.GetKey("s") && Input.GetKey("a"))
            {
                playerT.Translate(new Vector3(-0.75f, -0.75f, 0) * speed);
                tagDirection = PlayerDirection.downLeft;
            }
            else if (Input.GetKey("s") && Input.GetKey("d"))
            {
                playerT.Translate(new Vector3(0.75f, -0.75f, 0) * speed);
                tagDirection = PlayerDirection.downRight;
            }
            else if (Input.GetKey("w"))
            {
                playerT.Translate(new Vector3(0, 1, 0) * speed);
                tagDirection = PlayerDirection.up;
                playerAnim.Play("tagUp");
            }
            else if (Input.GetKey("d"))
            {
                playerT.Translate(new Vector3(1, 0, 0) * speed);
                tagDirection = PlayerDirection.right;
                playerSprite.flipX = true;
                playerAnim.Play("tagLeft");
            }
            else if (Input.GetKey("s"))
            {
                playerT.Translate(new Vector3(0, -1, 0) * speed);
                tagDirection = PlayerDirection.down;
                playerAnim.Play("tagDown");
            }
            else if (Input.GetKey("a"))
            {
                playerT.Translate(new Vector3(-1, 0, 0) * speed);
                tagDirection = PlayerDirection.left;
                playerSprite.flipX = false;
                playerAnim.Play("tagLeft");
            }
            else
            {
                if (tagDirection == PlayerDirection.up)
                {
                    playerAnim.Play("tagIdleUp");
                }
                else if (tagDirection == PlayerDirection.left)
                {
                    playerSprite.flipX = false;
                    playerAnim.Play("tagIdleLeft");
                }
                else if (tagDirection == PlayerDirection.right)
                {
                    playerSprite.flipX = true;
                    playerAnim.Play("tagIdleLeft");
                }
                else
                {
                    playerAnim.Play("tagIdle");//this is idle for down
                }
            }
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && FindObjectOfType<DialogueRunner>().isDialogueRunning != true)
        {
            playerEnabled = false;
            playerAnim.Play("tagIdle");
            CheckForNearbyNPC();
        }
        if(FindObjectOfType<DialogueRunner>().isDialogueRunning != true)
        {
            playerEnabled = true;
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
