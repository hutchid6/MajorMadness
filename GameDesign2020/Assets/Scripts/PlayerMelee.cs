using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public GameObject sword;
    float timeOfLastShot = 1f; //Cooldown


    private void Start() {
        sword.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
        //Increase value of time since last attack
        timeOfLastShot+= Time.deltaTime;

        //Attack on 'P', can be changed to any button
        //Check for cooldown
        if (Input.GetKey(KeyCode.P) && timeOfLastShot>1f){
            Attack();
            //reset cooldown
            timeOfLastShot = 0f;
            //show sword, after two seconds, call Disappear() and make it disappear
            sword.SetActive(true);
            Invoke("Disappear", 1);
        }

        
    }


    void Attack()
    {
        // Play an attack animation
        Debug.Log("SLASH");
        // Detect all enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Apply damage, will be implemented a bit later
        foreach(Collider2D enemy in hitEnemies){
        Debug.Log("We hit " + enemy.name);
        }

    }

    //Shows circle when Player object is selected, allows for it to show the range of attack based on attackPoint object
    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    //Make sword disappear
    void Disappear() {
        sword.SetActive(false);
    }

}





