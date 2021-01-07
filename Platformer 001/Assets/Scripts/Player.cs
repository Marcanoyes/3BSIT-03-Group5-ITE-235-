using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public delegate void DeadEventHandler();
public class Player : Character
{
    public GameObject CreditsUI;

    public Text newMissionUI2;
    public Image newMissionUI2Image;
    public Text newMissionUI1;
    public Image newMissionUI1Image;
    public GameObject newMissionUI;
    public Image ArtifactImagePickUp;
    public Text ArtifactTitlePickUp;
    public Text ArtifactDescPickUp;
    public GameObject PickUpUI;
    
    public GameObject missionReminder;
    public GameObject gameWipeUI;
    public Transform anvilPos;
    public GameObject anvilParticlesWIN;
    public GameObject anvilParticles;
    [SerializeField] GameObject gameWinUI;
    public bool ifCompleted;
    public GameObject winningArtifact;
    public bool setAnvil;
    [SerializeField] GameObject gameOverUI;
    public GameObject slash;
    public bool canInteract;
    public Animator slashAnim;
    public int activeHearts;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite goldHeart;


    public int killCount;
    public int maxEnemies;
    public Text killCountText;
    public Image EnemyIcon;


    public int artifCount;
    public int maxArtif;
    public Text artifCountText;
    public Image artifIcon;

    
    private static Player instance;
    public event DeadEventHandler Dead;
    public GameObject artif1used;
    public Slider healthBar;
    public Image fillHealth;
    public static Player Instance
    {

        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }

    }
    
    
    [SerializeField] private Transform[] groundPoints;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce;
    [SerializeField] private float immortalTime;
    [SerializeField] private bool airControl;
    public bool immortal = false;
    public static Vector3 startPos;
    private SpriteRenderer spriteRenderer;
    public BoxCollider2D playerHitbox { get; set; }
    public Rigidbody2D MyRigidbody { get; set; }
    
    public bool Slide { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }

    public int ammo;
    public int numOfAmmo;
    private float ammoTimer;
    private float ammoCooldown = 7f;
    public Slider ammoSlider;
    public Image ammoSliderImage;
    
    public Image[] magazine;
    public GameObject ammoImages;
    public Sprite fillAmmo;
    public Sprite emptyAmmo;

     // ==================================================================
    [Header("Artifact1")]
    public GameObject artif1effect;
    public bool hasArtif1;
    public Image artifactImage1;
    public float cooldown1 = 15;
    public float artifactDuration1;
    private float durationCount1;
    private float timeLeft1CD;
    public bool isCooldown = false;
    public KeyCode artifact1;
    public Text cooldownText;
    

    [Header("Artifact2")]
    public GameObject trailArtif2;
    public bool hasArtif2;
    public Image artifactImage2;
    public float cooldown2 = 20;
    public float artifactDuration2;
    public float durationCount2;
    public Slider durationSlider02;
    public Image durationSlider02fill;
    private float timeLeft2CD;
    public bool isCooldown2 = false;
    public KeyCode artifact2;
    public Text cooldownText2;

    [Header("Artifact3")]
    public bool Artif3Active = false;
    public bool hasArtif3;
    public Image artifactImage3;
    public float cooldown3 = 25;
    public float artifactDuration3;
    public float durationCount3;
    public Slider durationSlider03;
    public Image durationSlider03fill;
    private float timeLeft3CD;
    public bool isCooldown3 = false;
    public KeyCode artifact3;
    public Text cooldownText3;
    // ==================================================================
    public void Win()
    {
        gameWinUI.SetActive(true);
    }

    public void RollCredits()
    {
        CreditsUI.SetActive(true);
    }
    void Artifact1()
    {
        
        if(Input.GetKeyDown(artifact1) && isCooldown == false && hasArtif1 && !IsDead && PickUpUI.activeSelf != true)
        {
            if(health<50)
            {
                timeLeft1CD = cooldown1;
                isCooldown = true;
                artifactImage1.fillAmount = 1;
                cooldownText.fontSize = 47;
                Instantiate(artif1effect,transform.position,Quaternion.identity);
                Instantiate(artif1used,healthBar.transform.position,Quaternion.identity);
                cooldownText.gameObject.SetActive(true);
                health += 15;
                if(health >= 50)
                {
                    health = 50;
                }
                AudioManager.audioManager.Play("Artif1Sound");
            }
            else
            {
                missionReminder.GetComponent<Text>().text = "you have full HP";
                missionReminder.SetActive(true);
            }
        }

        if(isCooldown)
        {   
            //durationCount1 += Time.deltaTime;
            timeLeft1CD -= Time.deltaTime;
            artifactImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            cooldownText.text = (timeLeft1CD).ToString("0.00");
            if(artifactImage1.fillAmount<=0)
            {
                artifactImage1.fillAmount = 0;
                isCooldown = false;
                cooldownText.fontSize = 106;
                cooldownText.text = "U";
            }
            // if(durationCount1>=artifactDuration1)
            // {
            //     //immortal = false;
            // }
        }
    }
    void Artifact2()
    {
        if(Input.GetKeyDown(artifact2) && isCooldown2 == false && hasArtif2 && !IsDead && PickUpUI.activeSelf != true)
        {
            
            timeLeft2CD = cooldown2;
            isCooldown2 = true;
            artifactImage2.fillAmount = 1;
            cooldownText2.fontSize = 47;
            cooldownText2.gameObject.SetActive(true);
            jumpForce = 975f;
            trailArtif2.SetActive(true);
            durationSlider02.gameObject.SetActive(true);
            durationSlider02fill.color = Color.white;
            durationCount2 = 0;
            AudioManager.audioManager.Play("Artif2Sound");
        }

        if(isCooldown2)
        {
            durationCount2 += Time.deltaTime;
            timeLeft2CD -= Time.deltaTime;
            artifactImage2.fillAmount -= 1/ cooldown2 * Time.deltaTime;
            cooldownText2.text = (timeLeft2CD).ToString("0.00");
            durationSlider02.value = artifactDuration2 - durationCount2;
            if(durationSlider02.value < (durationCount2+1)/2)
            {
                durationSlider02fill.color = Color.red;
            }
            if(artifactImage2.fillAmount<=0)
            {
                artifactImage2.fillAmount = 0;
                isCooldown2 = false;
                cooldownText2.fontSize = 106;
                cooldownText2.text = "I";
            }
            if(durationCount2>=artifactDuration2)
            {jumpForce=625f;
            durationSlider02.gameObject.SetActive(false);
            durationSlider02.value = artifactDuration2;
            trailArtif2.SetActive(false);}
        }
    }

    void Artifact3()
    {
        if(Input.GetKeyDown(artifact3) && isCooldown3 == false && !immortal && hasArtif3 && !IsDead && PickUpUI.activeSelf != true)
        {
            
            timeLeft3CD = cooldown3;
            isCooldown3 = true;
            artifactImage3.fillAmount = 1;
            cooldownText3.fontSize = 47;
            cooldownText3.gameObject.SetActive(true);
            Artif3Active = true;
            durationSlider03.gameObject.SetActive(true);
            AudioManager.audioManager.Play("Artif3Sound");
            durationCount3 = 0;
            durationSlider03fill.color = Color.white;
        }

        if(isCooldown3)
        {
            durationCount3 += Time.deltaTime;
            timeLeft3CD -= Time.deltaTime;
            artifactImage3.fillAmount -=1/ cooldown3 * Time.deltaTime;
            cooldownText3.text = (timeLeft3CD).ToString("0.00");
            durationSlider03.value = artifactDuration3-durationCount3;
            if(durationSlider03.value < durationCount3/2)
            {
                durationSlider03fill.color = Color.red;
            }
            if(artifactImage3.fillAmount<=0)
            {
                artifactImage3.fillAmount = 0;
                isCooldown3 = false;
                cooldownText3.fontSize = 106;
                cooldownText3.text = "O";
            }
            if(durationCount3 >= artifactDuration3)
            {
                Artif3Active = false;
                durationSlider03.gameObject.SetActive(false);
                durationSlider03.value = artifactDuration3;
            }
        }
    }
    public void SlashActive()
    {
        if(slash.activeSelf == true)
        {slash.SetActive(false);}
        else
        {slash.SetActive(true);
        AudioManager.audioManager.Play("Slash");}
        
    }

    
    void UpdateHearts()
    {
          for(int x = 0; x<hearts.Length; x++)
          {
              if(x<activeHearts)
              {
                  if(Artif3Active == true)
                  {hearts[x].sprite = goldHeart;}
                  else
                  {hearts[x].sprite = fullHeart;}
                  
              }else
              {
                  hearts[x].sprite = emptyHeart;
              }

              if(x<numOfHearts)
              {
                  hearts[x].enabled = true;
              } else
              {
                  hearts[x].enabled = false;
              }
          }
    }
    void UpdateAmmo()
    {

        for(int i = 0; i < magazine.Length; i ++)
        {
            if(i < ammo)
            {
                magazine[i].sprite = fillAmmo;
            }else
            {
                magazine[i].sprite = emptyAmmo;
            }

            if(i < numOfAmmo)
            {
                magazine[i].enabled = true;
            }else
            {
                magazine[i].enabled = false;
            }

            
        }

           
        if(ammo <= 0)
        {
            
            // if(ammo == 2){scrollBG.color = new Color(1,0,0,1);}
            ammoTimer += Time.deltaTime;
            ammoSlider.value = ammoTimer;
            ammoSlider.gameObject.SetActive(true);
            if(ammoTimer <= ammoCooldown/2)
            {
                ammoSliderImage.color = Color.red;
            }
            else
            {
                ammoSliderImage.color = Color.white;
            }
            if(ammoTimer >= ammoCooldown)
            {
                ammoSlider.gameObject.SetActive(false);
                ammo += 5;
                
                ammoTimer = 0;
            }
        }
    }
    
    public override bool IsDead
    {
        get
        {
            if(health <= 0)
            {
                OnDeath();
            }
            return health <= 0;
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        hasArtif1 = false;
        hasArtif2 = false;
        hasArtif3 = false;
        canInteract = false;
        setAnvil = false;
        ifCompleted = false;

        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHitbox = GetComponent<BoxCollider2D>();
        MyRigidbody = GetComponent<Rigidbody2D>();
        movementSpeed = 7;
        startPos = MyRigidbody.position;
        maxArtif = 3;
        
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && PickUpUI.activeSelf)
            {
                ResumeGame();
                transform.position = transform.position;
                myAnimator.SetTrigger("idle");
            }
        Artifact1();
        Artifact2();
        Artifact3();

        killCountText.text = killCount.ToString()+" / "+maxEnemies;
        artifCountText.text = artifCount.ToString()+" / "+maxArtif;
        if(killCount == maxEnemies)
        {killCountText.color = Color.green;EnemyIcon.color = Color.green;
        killCountText.fontSize = 64;
        killCountText.text = "✓";}
        if(artifCount == maxArtif)
        {artifCountText.color = Color.green;artifIcon.color = Color.green;
        artifCountText.fontSize = 64;
        artifCountText.text = "✓";}
        if(killCount == maxEnemies && artifCount == maxArtif)
        {StartCoroutine(IndicateNewMission());}

        if(!TakingDamage && !IsDead)
        {
            // if(transform.position.y <= -16)
            // {
            //     Death();
            // }
            HandleInput();
        }
        UpdateHealth();
        UpdateAmmo();
        UpdateHearts();
        
        
    }

    public void PauseGame ()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame ()
    {
        PickUpUI.GetComponent<Animator>().SetTrigger("Close");
        Time.timeScale = 1;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!TakingDamage && !IsDead && !PickUpUI.activeSelf)
        {
            float horizontal = Input.GetAxis("Horizontal");
            OnGround = IsGrounded();
            HandleMovement(horizontal);
            Flip(horizontal);
            HandleLayers();
        }
    }

    public void OnDeath()
    {
        if(Dead != null)
        {
            slash.SetActive(false);
            TakeDamage();
            Dead();
            if(activeHearts>=2){gameWipeUI.SetActive(true);}
            
            
            
        }
    }
    

    private void HandleMovement(float horizontal)
    {
        if (MyRigidbody.velocity.y < 0) 
        {
            myAnimator.SetBool("land", true);
        }

        if (!Attack && !Slide || (OnGround || airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }

        if (Jump && OnGround)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleInput()
    {
        if(!IsDead && health>0)
        {
            if ((Input.GetKeyDown(KeyCode.W)) && !Slide)
            {
                myAnimator.SetTrigger("jump");
            }
            
            if (Input.GetKeyDown(KeyCode.J))
            {
                myAnimator.SetTrigger("attack");
            }

            if (Input.GetKeyDown(KeyCode.L) && OnGround && MyRigidbody.velocity.x != 0)
            {
                myAnimator.SetTrigger("slide");
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                if(ammo >= 1)
                {
                    myAnimator.SetTrigger("throw");
                }else
                {
                    missionReminder.GetComponent<Text>().text = "Reloading...";
                    missionReminder.SetActive(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.F) && canInteract == true)
            {
                if(ifCompleted == true)
                {
                    winningArtifact.SetActive(true);
                    setAnvil = true;
                    Player.Instantiate(anvilParticles,anvilPos.position,Quaternion.identity);
                    newMissionUI2.text = "✓";
                    newMissionUI2.color = Color.green;
                    newMissionUI2Image.color = Color.green;
                    AudioManager.audioManager.Play("ArtifCraft");


                }else
                {
                    missionReminder.GetComponent<Text>().text = "Finish Missions";
                    missionReminder.SetActive(true);
                }
                
            }
        }
        
    }

    private void Flip(float horizontal)
    {
        if(!Attack){
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }}
    }

    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if(!OnGround)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(0, 1);
        }
    }
    public override void Throw(int value)
    {
        if(ammo>=1)
        {
            ammoTimer = 0;

            if (!OnGround && value == 1 && myAnimator.GetCurrentAnimatorStateInfo(1).IsTag("jumpthrow") || OnGround && value == 0 )
            {
                base.Throw(value);
                ammo -= 1;
                //ammoTimer = 0;
            }

        }
    }

    public static float ToSingle(double value)
    {
        return (float)value;
    }

    private IEnumerator IndicateNewMission()
    {
        yield return new WaitForSecondsRealtime(1f);
        newMissionUI.GetComponent<Animator>().SetTrigger("NewMission");
    }

    public IEnumerator IndicateLastMission()
    {
        yield return new WaitForSecondsRealtime(1f);
        newMissionUI.GetComponent<Animator>().SetTrigger("LastLastMission");
    }

    private IEnumerator IndicateImmortal()
    {
        while(immortal)
        {
            spriteRenderer.color = new Color(1,1,1,0.5f);
            // spriteRenderer.enabled = false;
            yield return new WaitForSecondsRealtime(0.093f);
            spriteRenderer.color = Color.white;
            // spriteRenderer.enabled = true;
            yield return new WaitForSecondsRealtime(0.093f);
        }
    }








    public override IEnumerator TakeDamage()
    {   
        if(!immortal && Artif3Active == false)
        {
            health-=10;
            if(!IsDead)
            {
                AudioManager.audioManager.Play("Hurt");
                myAnimator.SetTrigger("damage");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
            }
            else
            {
                
                if(health<=0)
                {
                    AudioManager.audioManager.Play("PlayerDeath");
                    activeHearts -= 1;
                    myAnimator.SetLayerWeight(1,0);
                    myAnimator.SetTrigger("die");
                    immortal = true;
                }
                
            }

        }
        
    }
    

    public override void Death()
    {
        if(activeHearts>=1)
        {
            AudioManager.audioManager.Play("Respawn");
            health = 50;
            transform.position = startPos;
            immortal = false;
            myAnimator.SetTrigger("idle");
        }else
        {
            gameOverUI.SetActive(true);
        }
            
        
    }

    private void UpdateHealth()
    {
        if(Artif3Active == true)
        {
            fillHealth.color = Color.yellow;
        }else
        {
            if(health <=20)
            {
                fillHealth.color = Color.red;
            }
            else
            {
                fillHealth.color = Color.green;
            }

        }
        

        healthBar.value = health;
        
        // fillHealth.color = Color.green;
    }

    public void FireSound()
    {
        AudioManager.audioManager.Play("Fire");
    }

    
}
