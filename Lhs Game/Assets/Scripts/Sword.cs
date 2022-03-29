using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject player;
    public float swingWidth = 60;
    public float swingSpeed = 0.01f;
    private Camera cam;
    private int state = 0; // 0 = default, 1 = swinging
    private float swingDir;
    private float swingDuration;
    private List<GameObject> enemiesHit;

    void Awake() 
    {
        cam = Camera.main;
    }

    void Start()
    {
        enemiesHit = new List<GameObject>();
    }

    void Update()
    {
        if (player.GetComponent<Player>().moveLock)
        {
            return;
        }
        transform.rotation = Quaternion.identity;
        transform.position = player.transform.position;
        //transform.Translate(1, 0, 0);

        if (state == 0)
        {
            float rotation = faceMouse();
            
            if (Input.GetButtonDown("Fire1"))
            {
                state = 1;
                transform.Rotate(new Vector3(0,0,rotation + swingWidth));
                swingDir = rotation;
                swingDuration = swingWidth;
                enemiesHit.Clear();
            }
        }
        else if (state == 1)
        {
            if (swingDuration > -1 * swingWidth)
            {
                transform.Rotate(new Vector3(0,0,swingDir + swingDuration));
                swingDuration -= swingSpeed * Time.deltaTime;
            }
            else 
            {
                state = 0;
            }

        }
        transform.Translate(1, 0, 0);
    }

    private float faceMouse()
    {
        cam = Camera.main;
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = player.transform.position;
        float yDiff = playerPos.y - mousePos.y;
        float xDiff = playerPos.x - mousePos.x;
        float slope = yDiff / xDiff;
        float rotation = Mathf.Atan(slope) * 180 / Mathf.PI;
        if (playerPos.x > mousePos.x)
        {
            rotation += 180f;
        }
        transform.Rotate(0.0f, 0.0f, rotation, Space.Self);
        if (rotation >= 90)
        {
            transform.Rotate(180, 0, 0);
        }
        return rotation;
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (state == 1 && other.CompareTag("Enemy"))
        {
            foreach (GameObject enemy in enemiesHit)
            {
                if (enemy == other.gameObject)
                {
                    return;
                }
            }
            other.gameObject.GetComponent<meleeEnemy>().takeDamage(20);
            other.gameObject.GetComponent<meleeEnemy>().knockback(20);
            enemiesHit.Add(other.gameObject);
        }
    }
}
