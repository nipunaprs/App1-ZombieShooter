using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{

    public Camera playercam;
    public float range = 100f;
    public float bulletDamage = 25f;
    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Makes sure it stops unless continuing to hold button down.
        if (playerAnimator.GetBool("isShooting"))
        {
            playerAnimator.SetBool("isShooting", false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {

        playerAnimator.SetBool("isShooting", true);

        RaycastHit hit;

        Physics.Raycast(playercam.transform.position, transform.forward, out hit, range); //Stored in hit variable

        //Could have also done ZombieManager zombieManager = hit.transform.GetComponent<EnemyManager>(); --> this also verifies that u hit an enemy
        if(hit.collider.gameObject.tag == "Enemy")
        {
            hit.collider.gameObject.GetComponent<ZombieManager>().Shot(bulletDamage);
        }
            
                
    }

}
