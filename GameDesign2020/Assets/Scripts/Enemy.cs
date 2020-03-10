using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int health;

    [SerializeField]
    GameObject player;

    PlayerController playerScript;
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision == player.GetComponent<Collision2D>())
        {
            playerScript.LoseHalfHeart();
        }
    }
}
