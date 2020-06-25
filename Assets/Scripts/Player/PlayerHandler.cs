using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerHandler : Character
{
    #region Variables
    [Header("Physics")]
    public CharacterController controller;
    public float gravity = 20f;
    public Vector3 moveDirection;

    [Header("Level Data")]
    public int level = 0;

    [Header("Quests")]
    public float currentExp, neededExp, maxExp;
    public Quest quest; //Make this a list to have more quests

    [Header("Damage Flash and Death")]
    public Image damageImage;
    public Image deathImage;
    public Text deathText;
    public AudioClip deathClip;
    public AudioSource playersAudio;
    public Transform currentCheckPoint;
    public Color flashColour = new Color(1, 0, 0, 0.2f);
    public float flashSpeed = 5f;
    public static bool isDead;
    public bool isDamaged;
    public bool canHeal;
    public float healDelayTimer;

    [Header("Player UI")]
    public GameObject crosshairImage;

    public CanvasSet canvasSet;

    #endregion

    #region Behaviour
    void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
    }
    public void Movement()
    {
        if (!isDead)
        {
            if (controller.isGrounded)
            {
                #region Bind Movement Checks
                moveDirection.z = 0;
                moveDirection.x = 0;
                moveDirection.y = 0;
                if (Input.GetKey(KeyBindManager.keys["Forward"]))
                {
                    moveDirection.z++;
                }
                if (Input.GetKey(KeyBindManager.keys["Backward"]))
                {
                    moveDirection.z--;
                }
                if (Input.GetKey(KeyBindManager.keys["Left"]))
                {
                    moveDirection.x--;
                }
                if (Input.GetKey(KeyBindManager.keys["Right"]))
                {
                    moveDirection.x++;
                }
                #endregion
                moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    moveDirection *= sprint;
                else if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl))
                    moveDirection *= crouch;
                else
                    moveDirection *= speed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
    public void Update()
    {
        Movement();
        #region Bar Update

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].displayImage.fillAmount = Mathf.Clamp01(attributes[i].currentValue / attributes[i].maxValue);
        }
        #endregion
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.X))
        {
            DamagePlayer(5);
        }
#endif
        #region Damage Flash
        if (isDamaged && !isDead)
        {
            damageImage.color = flashColour;
            isDamaged = false;
        }
        else if (damageImage.color.a > 0)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        #endregion
        if (!canHeal)
        {
            healDelayTimer += Time.deltaTime;
            if (healDelayTimer >= 5)
            {
                canHeal = true;
            }
        }
        if (canHeal && attributes[0].currentValue < attributes[0].maxValue && attributes[0].currentValue > 0)
        {
            HealOverTime();
        }

        if (crosshairImage != null)
        {
            if (PauseMenu.isPaused)
            {
                crosshairImage.SetActive(false);
            }
            else
            {
                crosshairImage.SetActive(true);
            }
        }

    }
    public void DamagePlayer(float damage)
    {
        //Turn on the red flicker
        isDamaged = true;
        //take damage
        attributes[0].currentValue -= damage;
        //delay regen healing
        canHeal = false;
        healDelayTimer = 0;
        if (attributes[0].currentValue <= 0 && !isDead)
        {
            Death();
        }
    }
    public void HealOverTime()
    {
        attributes[0].currentValue += Time.deltaTime * (attributes[0].regenValue /*plus our constitution value??...Maybe another value*/);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            currentCheckPoint = other.transform;
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue += 7;
            }
            PlayerSaveAndLoad.Save();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue -= 7;
            }
            PlayerSaveAndLoad.Save();

        }
    }
    #endregion

    #region Quest
    public void KilledCreature(string enemyTag)
    {
        if (quest.goal.questState == QuestState.Active)
        {
            quest.goal.EnemyKilled(enemyTag);
        }
    }

    public void ItemsCollected(int ID)
    {
        if (quest.goal.questState == QuestState.Active)
        {
            quest.goal.ItemCollected(ID);
        }
    }
    #endregion

    #region Death and Respawn

    void Death()
    {
        //Set the death flag to dead
        isDead = true;
        //clear existing text just in case!
        deathText.text = "";
        //Set and Play the audio clip
        playersAudio.clip = deathClip;
        playersAudio.Play();
        //Trigger death screen
        deathImage.GetComponent<Animator>().SetTrigger("isDead");
        //in 2 seconds set our text when we die
        Invoke("DeathText", 2f);
        //in 6 seconds set our text when we respawn
        Invoke("RespawnText", 6f);
        //in 9 seconds respawn us
        Invoke("Respawn", 9f);
    }
    void DeathText()
    {
        deathText.text = "You've Fallen in Battle...";
    }
    void RespawnText()
    {
        deathText.text = "...But the Gods have decided it is not your time...";
    }
    void Respawn()
    {
        //RESET EVERYTHING!
        deathText.text = "";
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].currentValue = attributes[i].maxValue;
        }
        isDead = false;
        //load position
        this.transform.position = currentCheckPoint.position;
        this.transform.rotation = currentCheckPoint.rotation;
        //Respawn
        deathImage.GetComponent<Animator>().SetTrigger("Respawn");
    }
    #endregion
}
