using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    private Camera cam;
    public GameObject player;

    public float speed = 0.8f;
    private float distance = 0.0f;

    void Awake() 
    {
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 playerLoc = player.transform.position;
        transform.Translate(playerLoc);
        cam.transform.Translate(playerLoc);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(player.transform);
        Vector3 camPos = transform.position;
        Vector3 playerPos = player.transform.position;
        float yDiff = playerPos.y - camPos.y;
        float xDiff = playerPos.x - camPos.x;
        float slope = yDiff / xDiff;
        float rotation = Mathf.Atan(slope) * 180 / Mathf.PI;
        if (playerPos.x > camPos.x)
        {
            rotation += 180f;
        }
        transform.rotation = Quaternion.identity;
        transform.Rotate(0.0f, 0.0f, rotation + 180, Space.Self);


        distance = Vector3.Distance(transform.position, player.transform.position);

        Vector3 movement = new Vector3(Mathf.Pow(distance,2), 0, 0);
        movement *= Time.deltaTime * speed * 1;

        transform.Translate(movement);

        cam.transform.position = new Vector3(transform.position.x,
                                            transform.position.y,
                                            cam.transform.position.z);
    }
}
