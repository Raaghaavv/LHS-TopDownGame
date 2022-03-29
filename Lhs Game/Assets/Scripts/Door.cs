using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string nextScene;
    public Vector3 playerPos;
    public TransferValues memory;

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            memory.setHealth(other.gameObject.GetComponent<Player>().currentHealth);
            memory.setMaxHealth(other.gameObject.GetComponent<Player>().maxHealth);
            memory.setMana(other.gameObject.GetComponent<Player>().currentMana);
            memory.setMaxMana(other.gameObject.GetComponent<Player>().maxMana);
            memory.setSpawnPos(playerPos);
            SceneManager.LoadScene(nextScene);
        }
    }
}
