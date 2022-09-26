using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    //Character health variable
    public float health = 100;

    public GameManager gameManager;


    public void Hit(float damage){
        health -= damage;
        Debug.Log(health);
        if(health <= 0)
        {
            gameManager.EndGame();
        }
    }

}
