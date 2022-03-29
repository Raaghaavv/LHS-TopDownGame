using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private enum State {
        Normal,
        Rolling,
    }
    public GameObject player;
    public int maxHealth; // maximum health
    public int currentHealth;
    public healthBar healthbar;
    public int maxMana;
    public int currentMana;
    public manaBar manabar;

    public float speed;
    public GameObject gun;
    public GameObject sword;
    public bool moveLock;
    public TransferValues transfer;

    //Private fields
    private Rigidbody2D rb;
    private Vector3 rollDir;
    private float rollSpeed;
    private State state;
    private int currentWeapon; //0=gun, 1=sword
    private GameObject activeWeapon;
    private Vector3 input;
    private bool dashInput;

    //Start is called before the first frame update
    void Start()
    {
        if (!transfer.healthInitialized)
        {
            transfer.setHealth(maxHealth);
            transfer.setMana(maxMana);
            transfer.healthInitialized = true;
        }
        currentHealth = transfer.getHealth(); // when the game starts, the health will be at maximum
        healthbar.setMaxHealth(maxHealth); // calls SetMaxHealth in healthBar.cs
        healthbar.setHealth(currentHealth);

        currentMana = transfer.getMana();
        manabar.setMaxMana(maxMana);
        manabar.setMana(currentMana);

        transform.position = transfer.getSpawnPos();
        transfer.setSpawnPos(new Vector3(0,0,0));
        moveLock = false;
        currentWeapon = 0;
        dashInput = false;
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        state = State.Normal;
        GameObject newGun = Instantiate(gun);
        newGun.GetComponent<Gun>().player = this.gameObject;
        activeWeapon = newGun;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        input = new Vector3(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.F) && state != State.Rolling)
        {
            dashInput = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentWeapon == 0)
            {
                Destroy(activeWeapon);
                activeWeapon = Instantiate(sword);
                activeWeapon.GetComponent<Sword>().player = this.gameObject;
                currentWeapon = 1;
            }
            else if (currentWeapon == 1)
            {
                Destroy(activeWeapon);
                activeWeapon = Instantiate(gun);
                activeWeapon.GetComponent<Gun>().player = this.gameObject;
                currentWeapon = 0;
            }
        }
    }

    void FixedUpdate() 
    {
        if (!moveLock)
        {
            move();
        }
        else
        {
            rb.velocity = new Vector2(0,0);
        }
    }

    public void move()
    {
        switch (state) {
        case State.Normal:
            rb.MovePosition(transform.position + input * speed * Time.deltaTime);

            if (dashInput) {
                rollDir = input;
                rollSpeed = 35;
                state = State.Rolling;
            }
            break;
        
        case State.Rolling:
            float rollSpeedDropMultiplier = 160f;
            rollSpeed -=  rollSpeedDropMultiplier * Time.deltaTime;

            float rollSpeedMinimum = 12f;
            if (rollSpeed < rollSpeedMinimum) {
                state = State.Normal;
                dashInput = false;
            }
            rb.MovePosition(transform.position + rollDir * rollSpeed * Time.deltaTime);
            break;
        }  
    }
    // TEST
    public void knockback(float angle) // when the player touches the enemy this calls. Move player back by a bit. Set Enemy state to .
    {
        player = GameObject.FindWithTag("Player");
        Vector3 playerPos = player.transform.position;
            Vector3 enemyPos = transform.position;
//            float yDiff = enemyPos.y - playerPos.y;
//            float xDiff = enemyPos.x - playerPos.x;
//            float slope = yDiff / xDiff;
//            float rotation = Mathf.Atan(slope) * 180 / Mathf.PI;
//            if (enemyPos.x > playerPos.x)
//            {
//                rotation += 180f;
//            }
            //transform.Rotate(0.0f, 0.0f, rotation, Space.Self);
            //rotation*=-1;
            //transform.Rotate(new Vector3(0,0,rotation));
            Vector3 move1 = new Vector3(20*Mathf.Cos(angle),20*Mathf.Sin(angle),0); // x,y,z
            move1 *= speed *  Time.deltaTime;
            transform.Translate(move1);
    }
    //TEST
    public void takeDamage(int damage){ // whenever we take damage, our current health goes down by "damage" amount
        currentHealth-=damage;
        healthbar.setHealth(currentHealth);
    }

    // This method is for the gun. If the player has enough mana this returns
    // true and subtracts the mana cost of firing from the player's mana.
    public bool canFire(int manaCost)
    {
        if (manaCost <= currentMana)
        {
            currentMana -= manaCost;
            manabar.setMana(currentMana);
            return true;
        }   
        return false;
    }
}
