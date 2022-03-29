using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart : MonoBehaviour
{
    public static OnStart Instance;
    public TransferValues transfer;

    void Awake()
    {
        if (Instance!=null) { 
            Destroy(gameObject); 
            return; 
        } // stops dups running
        DontDestroyOnLoad(gameObject); // keep me forever
        Instance = this; // set the reference to it

        //... code to run only once ...
        transfer.setHealth(transfer.getMaxHealth());
        transfer.setMana(transfer.getMaxMana());
        transfer.healthInitialized = false;
    }
    void Start()
    {
        transfer.setHealth(transfer.getMaxHealth());
        transfer.setMana(transfer.getMaxMana());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
