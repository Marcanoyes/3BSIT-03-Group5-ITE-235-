using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDragon : Character
{   
    
    [SerializeField] public Slider healthBar;
    public Image fillHealth;
    [SerializeField] private int maxHPofEnemy;
    [SerializeField] private RectTransform healthTransform;
    [SerializeField] public Transform groundDetection;
    [SerializeField] private Transform sightCast;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] public GameObject effect;
    private IEnemyState currentState;
    public GameObject Target { get; set; }
    [SerializeField] private float meleeRange;
    [SerializeField] private float throwRange;
    public static float EnemyMovementSpeed;
    private Vector2 direction;
    public float agroTimer;

    private void OnSightTarget()
    {
        if(facingRight == true){direction = Vector2.right;}
        else{direction = Vector2.left;}

        RaycastHit2D enemyInfo = Physics2D.Raycast(sightCast.position,direction,10f);
        if(enemyInfo.collider == true)
        {
            if(enemyInfo.transform.tag == "Player")
            {
                agroTimer = 0;
                this.Target = GameObject.FindGameObjectWithTag("Player");
                // Debug.Log ("TARGET LOCKED");
            }
            else
            {
                agroTimer += Time.deltaTime;
                if(agroTimer >= 3f)
                {this.Target = null;
                // Debug.Log("NO TARGET");
                }

            }
        }

    }

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
    public bool InMeleeRange
    {
        get
        {
            if(Target!=null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }


    public bool InThrowRange
    {
        get
        {
            if(Target!=null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
            }
            return false;
        }
    }
    public override bool IsDead
    {
        get
        {
            return health<=0;
        }

    }
    // Start is called before the first frame update
    public override void Start()
    {   
        
        base.Start();
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        ChangeState(new IdleState());
        EnemyMovementSpeed = 2;
        this.facingRight = true;
        Player.Instance.maxEnemies += 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(agroTimer.ToString("0.00"));
        if(!IsDead)
        {
            if(!TakingDamage)
            {
                currentState.Execute();
                OnSightTarget();
            }
            
            LookAtTarget();
        }
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position,Vector2.down,0.5f);
        if(groundInfo.collider == false)
        {
            EnemyChangeDirection();
            // RemoveTarget();
        }
        RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position,groundDetection.position,0.3f);
        if(wallInfo.collider == true)
        {
            if(wallInfo.collider.CompareTag("Edge"))
            {
                EnemyChangeDirection();
                // RemoveTarget();
            }
        }
        SetHealth();
    }

    public void RemoveTarget()
    {
        Target = null;
        // ChangeState(new PatrolState());
    }

    private void LookAtTarget()
    {
        if (Target!=null)
        {
            float xDirection = Target.transform.position.x - transform.position.x;

            if(xDirection < 0 && facingRight || xDirection > 0 && !facingRight)
            {
                EnemyChangeDirection();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter(this);
    }

    public void Move()
    {
        if(!Attack)
        {
            myAnimator.SetFloat("speed", 1);
            transform.Translate(GetDirection() * (EnemyMovementSpeed * Time.deltaTime));
        }
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right: Vector2.left;
    }
    public override void OnTriggerEnter2D(Collider2D other) 
    {

        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
        
    }

    public override IEnumerator TakeDamage()
    {
        StartCoroutine(Character.FlashDamage(bodyRenderer));
        health -= 10;
        
        if(!IsDead)
        {
            Target = GameObject.FindGameObjectWithTag("Player");
            myAnimator.SetTrigger("damage");
        }
        else
        {
            myAnimator.SetTrigger("die");
            yield return null;
        }
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void Death()
    {
        Instantiate(effect,transform.position,Quaternion.identity);
        Player.Instance.killCount += 1;
        Destroy(gameObject);
    }

    public override void Throw(int value)
    {
        base.Throw(value);
    }

    private void EnemyChangeDirection()
    {
        // Debug.Log(this.facingRight.ToString());
        ChangeDirection();
        healthTransform.localScale = new Vector3(transform.localScale.x * -1,1,1);
        Target = null;
    }


    
}
