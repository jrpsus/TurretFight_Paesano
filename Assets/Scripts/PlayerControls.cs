using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public float speed = 4.0f;
    public float projectileSpeed = 16f;
    public int health = 5;
    public Text healthText;
    public Rigidbody2D rb;
    public GameObject projectile;
    public Transform firePoint;
    public Vector2 mousePos;
    public Vector2 movement;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        //transform.position = newPosition;
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb2 = bullet.GetComponent<Rigidbody2D>();
        rb2.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            health -= 1;
            healthText.text = "HEALTH: " + health;
            if (health <= 0)
            {
                SceneManager.LoadScene("YouDied");
            }
        }
    }
}
