using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoggyPatrol : MonoBehaviour
{
    private Animator dogAnimator;
    public static SpriteRenderer doggyBody;
    [SerializeField] public GameObject effect;
    public static bool immortal;
    [SerializeField] private int health;
    [SerializeField] public Slider healthBar;
    public Image fillHealth;
    [SerializeField] private int maxHPofEnemy;
    [SerializeField] private RectTransform healthTransform;
    public static float speed;
    private bool movingRight;
    public Transform groundDetection;
    public Transform sightDetection;
    private bool hasTarget;
    private Vector2 direction;

    private void SetHealth()
    {
        healthBar.gameObject.SetActive(health < maxHPofEnemy);
        healthBar.value = health;
        healthBar.maxValue = maxHPofEnemy;

        if(health <= (maxHPofEnemy/2))
        {
            fillHealth.color = Color.red;
        }
        else
        {
            fillHealth.color = Color.white;
        }
    }

    private void Start() {
        doggyBody = GetComponent<SpriteRenderer>();
        immortal = false;
        movingRight = true;
        direction = Vector2.right;
        dogAnimator = GetComponent<Animator>();
        health = maxHPofEnemy;
        Player.Instance.maxEnemies += 1;
        
    }
    void Update() 
    {
        
        Patrol();

        SetHealth();

        
        
    }
    IEnumerator hurt()
        {
            immortal = true;
            speed = 0;
            doggyBody.color = Color.red;
            yield return new WaitForSecondsRealtime(0.3f);
            DoggyPatrol.immortal = false;
            speed = 3;
            doggyBody.color = Color.white;
            }
    void ChangeDirection()
    {
        if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                healthTransform.localScale = new Vector3(transform.localScale.x * 1,1,1);
                movingRight = false;
                direction = Vector2.left;
            }else
            {
                transform.eulerAngles = new Vector3(0,0,0);
                healthTransform.localScale = new Vector3(transform.localScale.x * -1,1,1);
                movingRight = true;
                direction = Vector2.right;
            }

    }
    void Patrol()
    {
        if(hasTarget && immortal == false)
        {
            speed = 5;
            dogAnimator.speed = 1f;
        }else
        {
            if(immortal == false)
            {
                speed = 3;
                dogAnimator.speed = 0.65f;
            }
        }

        if(immortal == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position,Vector2.down,1f);
        if(groundInfo.collider == false)
        {
            ChangeDirection();
        }


        Sight(direction);
        // if(movingRight == true)
        // {
        //     RaycastHit2D sightInfo = Physics2D.Raycast(sightDetection.position,Vector2.right,10f);
        //     if(sightInfo.collider == true)
        //     {
        //         if(sightInfo.transform.tag == "Player")
        //         {
        //             hasTarget = true;
        //         }else
        //         {
        //             hasTarget = false;
        //         }
            
        //     }   

        // }else
        // {
        //     RaycastHit2D sightInfo = Physics2D.Raycast(sightDetection.position,Vector2.left,10f);
        //     if(sightInfo.collider == true)
        //     {
        //         if(sightInfo.transform.tag == "Player")
        //         {
        //             hasTarget = true;
        //         }else
        //         {
        //             hasTarget = false;
        //         }
            
        //     }  
        // }
        


        RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position,groundDetection.position,0.01f);
        if(wallInfo.collider == true)
        {
            if(wallInfo.collider.CompareTag("Edge"))
            {
                ChangeDirection();
            }
            
        }

    }

    void Sight(Vector2 dir)
    {
        
        RaycastHit2D sightInfo = Physics2D.Raycast(sightDetection.position,dir,10f);
        if(sightInfo.collider == true)
        {
            if(sightInfo.transform.tag == "Player")
            {
                hasTarget = true;
            }else
            {
                hasTarget = false;
            }
            
        }   
            
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerProjectile") || other.CompareTag("Sword"))
        {
            if(immortal == false)
            {
                dogAnimator.SetTrigger("hit");
                speed = 0;
                health-=10;
                StartCoroutine(hurt());
                // if(other.CompareTag("Sword")){ChangeDirection();}
             }
            if(health<=0)
            {
                AudioManager.audioManager.Play("DogDeath");
                Player.Instance.killCount += 1;
                DoggyPatrol.immortal = false;
                speed = 3;
                doggyBody.color = Color.white;
                this.gameObject.SetActive(false);
            //    Destroy(this.gameObject);
               Instantiate(effect,transform.position,Quaternion.identity);
            }
        
        }
    }
}
