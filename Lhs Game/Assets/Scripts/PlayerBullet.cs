using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Update is called once per frame
    public int speed;
    public int damage = 20;
    void Update()
    {
        Vector3 movement = new Vector3(0, 1, 0);
        movement *= Time.deltaTime * speed;
        transform.Translate(movement);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<meleeEnemy>().takeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    public int getDamage()
    {
        return damage;
    }
}
