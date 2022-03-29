using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject textBox, textInBox, exclamation;
    public TextAsset dialogue;
    private GameObject player, exclamation1;
    private string dialogueStr;
    private bool dialogueOn, playerInRange, creatingText, clicked;

    
    void Start()
    {
        dialogueStr = dialogue.ToString();
        dialogueOn = false;
        playerInRange = false;
        creatingText = false;
        clicked = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {   
            if (creatingText)
            {
                clicked = true;
            }
            else if (playerInRange)
            {
                createText();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
            Vector3 thisPos = transform.position;
            thisPos.y += 1;
            exclamation1 = Instantiate(exclamation, thisPos, Quaternion.identity);
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Destroy(exclamation1);
        }
    }

    protected void createText()
    {
        if (!dialogueOn){
            textBox.transform.localScale = new Vector3(1,1,1);
            textInBox.transform.localScale = new Vector3(1,1,1);
            //textInBox.GetComponent<Text>().text = dialogueStr;
            StartCoroutine(createTextSlow(textInBox.GetComponent<Text>()));
            dialogueOn = true;
            player.GetComponent<Player>().moveLock = true;
        } else {
            textBox.transform.localScale = new Vector3(0,0,0);
            textInBox.transform.localScale = new Vector3(0,0,0);
            dialogueOn = false;
            player.GetComponent<Player>().moveLock = false;
        }
    }

    private IEnumerator createTextSlow(Text text)
    {
        creatingText = true;
        text.text = "";
        for (int i = 0; i < dialogueStr.Length; i++)
        {
            text.text = text.text + dialogueStr[i];
            yield return new WaitForSeconds(0.02f);
            if (clicked)
            {
                text.text = dialogueStr;
                break;
            }
        }
        creatingText = false;
        clicked = false;
    }
}
