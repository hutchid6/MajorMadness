using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector2 direction;
    PlayerController playerControl;
    Rigidbody2D rig;

    //Shooting Sound
    //public AudioClip Shootsound;

    public bool optionalcooldown;

    int cooldown = 0;

    //can add a trail to projectile
    //public GameObject trail;
    //projectile being shot
    public Rigidbody2D projectile;
    //velocity of projectile
    public int velocity1 = 500;

    void Start()
    {
        //gets rigidbody
        rig = gameObject.GetComponent<Rigidbody2D>();
        playerControl = GetComponent<PlayerController>();
    }

    void Update()
    {
        //gets mousepostition and direction relative to character
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // direction = (mousePosition - transform.position).normalized;

        //these if else statements will make the player shoot in the direction he is facing
        if (playerControl.tagDirection == PlayerController.PlayerDirection.up)
        {
            direction = new Vector3(0, 1, 0);
        }
        else if(playerControl.tagDirection == PlayerController.PlayerDirection.upRight)
        {
            direction = new Vector3(1, 1, 0);
        }
        else if (playerControl.tagDirection == PlayerController.PlayerDirection.right)
        {
            direction = new Vector3(1, 0, 0);
        }
        else if (playerControl.tagDirection == PlayerController.PlayerDirection.downRight)
        {
            direction = new Vector3(1, -1, 0);
        }
        else if (playerControl.tagDirection == PlayerController.PlayerDirection.down)
        {
            direction = new Vector3(0, -1, 0);
        }
        else if (playerControl.tagDirection == PlayerController.PlayerDirection.downLeft)
        {
            direction = new Vector3(-1, -1, 0);
        }
        else if (playerControl.tagDirection == PlayerController.PlayerDirection.left)
        {
            direction = new Vector3(-1, 0, 0);
        }
        else if (playerControl.tagDirection == PlayerController.PlayerDirection.upLeft)
        {
            direction = new Vector3(-1, 1, 0);
        }

        if (Input.GetMouseButtonDown(0) && cooldown == 0)
        {
            if (optionalcooldown == true)
            {
                cooldown = 1;
            }

            Shoot();

        }

        //Check if Cooldown is ready if not run Cooldown Clock
        if (cooldown > 0)
        {
            StartCoroutine(CountDown());
        }
        if (cooldown == 0)
        {
            StopAllCoroutines();
        }
    }
    //Cooldown Clock
    IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (cooldown > 0)
            {
                cooldown -= 1;
            }

        }

    }

    void Shoot()
    {
        //turn position to 2d
        Vector3 pos = transform.position;
        float x = pos.x;
        float y = pos.y;
        Vector2 pos2D = new Vector2(x, y);
        Vector2 ShootPos = pos2D + (direction);

        Vector3 velocity;

        if (playerControl.tagDirection == PlayerController.PlayerDirection.upLeft ||
            playerControl.tagDirection == PlayerController.PlayerDirection.upRight ||
            playerControl.tagDirection == PlayerController.PlayerDirection.downLeft ||
            playerControl.tagDirection == PlayerController.PlayerDirection.downRight)
        {
           velocity = new Vector3(15, 15, 0);
        }
        else
        { 
           velocity = new Vector3(20, 20, 0);
        }

        //ShootPos += new Vector2(0, 1, 0);
        Rigidbody2D project = Instantiate(projectile, ShootPos, Quaternion.Euler(0, 0, 0));
        //GameObject traila = Instantiate(trail, ShootPos, Quaternion.Euler(80, 120, 0));
        //traila.transform.SetParent(project.transform);
        project.velocity = transform.TransformDirection(direction * velocity.magnitude);
        //AudioSource.PlayClipAtPoint(Shootsound, transform.position);

    }

}


