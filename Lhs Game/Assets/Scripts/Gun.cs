using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject player;
    private Camera cam;
    public GameObject bullet;
    public int manaCost = 10;

    void Awake() 
    {
        cam = Camera.main;
    }

    // Start is called before the first frame update lol shivam was here
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().moveLock)
        {
            return;
        }
        transform.rotation = Quaternion.identity;
        float rotation = faceMouse();
        transform.position = player.transform.position;
        transform.Translate(1, 0, 0);
        if (rotation >= 90)
        {
            transform.Rotate(180, 0, 0);
        }


        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }

    private void shoot()
    {
        if (player.GetComponent<Player>().canFire(manaCost))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.transform.Rotate(new Vector3(0,0,-90));
        }
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
        return rotation;
    }
}
