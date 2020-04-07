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
    public bool playerEnabled = true;
    Vector3 mousePosition;
    Vector3 direction;
    bool playerMoving = false;

    public enum PlayerDirection
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
    public PlayerDirection tagDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerT = GetComponent<Transform>();
        playerAnim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();

        tagDirection = PlayerDirection.up;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerEnabled)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
            if(direction.y > 0 && direction.x > -0.5f && direction.x < 0.5)
            {
                tagDirection = PlayerDirection.up;
            }
            else if(direction.y < 0 && direction.x > -0.5f && direction.x < 0.5)
            {
                tagDirection = PlayerDirection.down;
            }

            if(direction.x < 0 && direction.y >= -0.2 && direction.y <= 0.2)
            {
                tagDirection = PlayerDirection.left;
            }
            else if(direction.x > 0 && direction.y >= -0.2 && direction.y <= 0.2)
            {
                tagDirection = PlayerDirection.right;
            }
            Debug.Log(tagDirection);
            Debug.Log(direction);

            if (Input.GetKey("w") && Input.GetKey("a"))
            {
                playerT.Translate(new Vector3(-0.75f, 0.75f, 0) * speed);
                playerMoving = true;
            }
            else if(Input.GetKey("w") && Input.GetKey("d"))
            {
                playerT.Translate(new Vector3(0.75f, 0.75f, 0) * speed);
                playerMoving = true;
            }
            else if (Input.GetKey("s") && Input.GetKey("a"))
            {
                playerT.Translate(new Vector3(-0.75f, -0.75f, 0) * speed);
                playerMoving = true;
            }
            else if (Input.GetKey("s") && Input.GetKey("d"))
            {
                playerT.Translate(new Vector3(0.75f, -0.75f, 0) * speed);
                playerMoving = true;
            }
            else if (Input.GetKey("w"))
            {
                playerT.Translate(new Vector3(0, 1, 0) * speed);
                playerMoving = true;
            }
            else if (Input.GetKey("d"))
            {
                playerT.Translate(new Vector3(1, 0, 0) * speed);
                playerMoving = true;
            }
            else if (Input.GetKey("s"))
            {
                playerT.Translate(new Vector3(0, -1, 0) * speed);
                playerMoving = true;
            }
            else if (Input.GetKey("a"))
            {
                playerT.Translate(new Vector3(-1, 0, 0) * speed);
                playerMoving = true;
            }
            else
            {
                playerMoving = false;
            }

            if(playerMoving)
            {
                if(tagDirection == PlayerDirection.up)
                {
                    playerAnim.Play("tagUp");
                }
                else if(tagDirection == PlayerDirection.right)
                {
                    playerSprite.flipX = true;
                    playerAnim.Play("tagLeft");
                }
                else if(tagDirection == PlayerDirection.down)
                {
                    playerAnim.Play("tagDown");
                }
                else if(tagDirection == PlayerDirection.left)
                {
                    playerSprite.flipX = false;
                    playerAnim.Play("tagLeft");
                }
            }
            else
            {
                if (tagDirection == PlayerDirection.up)
                {
                    playerAnim.Play("tagIdleUp");
                }
                else if (tagDirection == PlayerDirection.right)
                {
                    playerSprite.flipX = true;
                    playerAnim.Play("tagIdleLeft");
                }
                else if (tagDirection == PlayerDirection.down)
                {
                    playerAnim.Play("tagIdle");
                }
                else if (tagDirection == PlayerDirection.left)
                {
                    playerSprite.flipX = false;
                    playerAnim.Play("tagIdleLeft");
                }
            }

        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && FindObjectOfType<DialogueRunner>().isDialogueRunning != true)
        {
            playerEnabled = false;
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
