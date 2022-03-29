using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TransferValues : ScriptableObject
{
    //public bool initialized;
    private Vector3 playerSpawnPos;
    private int health;
    private int maxHealth;
    private int mana;
    private int maxMana;
    
    public bool healthInitialized;

    //Getter & setter for max health
    public int getMaxHealth(){return maxHealth;}
    public void setMaxHealth(int newMaxHealth){maxHealth = newMaxHealth;}

    //Getter & setter for health
    public int getHealth(){return health;}
    public void setHealth(int newHealth){health = newHealth;}

    //Getter & setter for max mana
    public int getMaxMana(){return maxMana;}
    public void setMaxMana(int newMaxMana){maxMana = newMaxMana;}

    //Getter & setter for mana
    public int getMana(){return mana;}
    public void setMana(int newMana){mana = newMana;}

    public Vector3 getSpawnPos(){return playerSpawnPos;}
    public void setSpawnPos(Vector3 spawnPos){playerSpawnPos = spawnPos;}
}
