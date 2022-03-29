using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TransferValues transfer;
    public GameObject Canvas;
    public GameObject Player;
    public GameObject camMove;

    //private bool initialized;

    void Awake()
    {
        //Creates a player, cameraMove, and the canvas with the UI elements.
        GameObject player = Instantiate(Player, transfer.getSpawnPos(), Quaternion.identity);
        GameObject canvas = Instantiate(Canvas);
        GameObject cameraMove = Instantiate(camMove);

        //Initializes the player's healthBar variable
        player.GetComponent<Player>().healthbar = canvas.transform.GetChild(0).gameObject.GetComponent<healthBar>(); 
        player.GetComponent<Player>().manabar = canvas.transform.GetChild(1).gameObject.GetComponent<manaBar>(); 


        //Initializes cameraMove's player variable
        cameraMove.GetComponent<CameraMove>().player = player;

        GameObject [] NPCs = GameObject.FindGameObjectsWithTag("NPC");

        GameObject textBox = canvas.transform.GetChild(2).gameObject;
        GameObject textInBox = textInBox = canvas.transform.GetChild(3).gameObject;

        foreach ( GameObject npc in NPCs)
        {
            npc.GetComponent<NPC>().textBox = textBox;
            npc.GetComponent<NPC>().textInBox = textInBox;
        }

        textBox.transform.localScale = new Vector3(0,0,0);
        textInBox.transform.localScale = new Vector3(0,0,0);
    }


    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
