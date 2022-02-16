using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerControls playerControls;
    void Start()
    {
        playerControls = GameObject.Find("Player").GetComponent<PlayerControls>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerControls.health += 1;
            playerControls.healthText.text = "HEALTH: " + playerControls.health;
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
