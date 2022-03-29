using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeEnemy : MonoBehaviour
{
    public int health;
    public GameObject player;
    public float speed = 2;
    public int damage = 20;
    private int state; //0-wait, 1-move towards player, 2-dash towards player, 3-getting kb'd
    private float timer;
    private float timerLimit;
    private int dashTimer;
    private float kbLeft;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        state = 0;
        timerLimit = Random.Range(0.5f, 1.25f);
        timer = 0;
        dashTimer = 0;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                break;
            case 1:
                Vector3 move1 = new Vector3(1,0,0);
                move1 *= speed *  Time.deltaTime;
                transform.Translate(move1);
                break;
            case 2:
                Vector3 move2 = new Vector3(2,0,0);
                move2 *= speed *  Time.deltaTime;
                transform.Translate(move2);
                break;
            case 3:
                Vector3 move3 = new Vector3(-1,0,0);
                move3 *= Time.deltaTime * kbLeft;
                kbLeft -= Time.deltaTime * 80;
                transform.Translate(move3);
                break;
        }


        timer += Time.deltaTime;
        if (timer >= timerLimit)
        {
            if (state == 0)
            {
                transform.rotation = Quaternion.identity;
                if (Vector2.Distance(transform.position, player.transform.position) <= 2 && dashTimer <= 0)
                {
                    transform.Rotate(new Vector3(0,0,facePlayer()));
                    state = 2;
                    timerLimit = 0.5f;
                    dashTimer = 3;
                }
                else
                {
                    float rotation = facePlayer() + Random.Range(-40, 40);
                    transform.Rotate(new Vector3(0,0,rotation));
                    state = 1;
                    timerLimit = Random.Range(1.5f, 2.25f);
                    dashTimer--;
                }
            }

            else if (state == 1 || state == 2 || state == 3)
            {
                state = 0;
                timerLimit = Random.Range(0.75f, 1.75f);
            }
            timer = 0;
        }
    }

    private float facePlayer()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        float yDiff = enemyPos.y - playerPos.y;
        float xDiff = enemyPos.x - playerPos.x;
        float slope = yDiff / xDiff;
        float rotation = Mathf.Atan(slope) * 180 / Mathf.PI;
        if (enemyPos.x > playerPos.x)
        {
            rotation += 180f;
        }
        //transform.Rotate(0.0f, 0.0f, rotation, Space.Self);
        return rotation;
    }

    public void takeDamage(int damageDealt)
    {
        health -= damageDealt;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void knockback(int distance)
    {
        kbLeft = distance;
        timerLimit = 0.25f;
        timer = 0;
        state = 3;
        transform.Rotate(new Vector3(0,0,facePlayer()));
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag.Equals("Player"))
        {
                Vector3 playerPos = player.transform.position;
                Vector3 enemyPos = transform.position;
                float yDiff = enemyPos.y - playerPos.y;
                float xDiff = enemyPos.x - playerPos.x;
                float slope = yDiff / xDiff;

                float knockBackAngle = Mathf.Atan(slope);
                       if (enemyPos.x > playerPos.x)
                        {
                            knockBackAngle *=-1;
                        }
                knockBackAngle+=180f;
            other.gameObject.GetComponent<Player>().knockback(knockBackAngle); // Good news. When the enemy touches the player, this calls. We now just need to add the actual knockback.
            other.gameObject.GetComponent<Player>().takeDamage(damage);
        }
    }
}
