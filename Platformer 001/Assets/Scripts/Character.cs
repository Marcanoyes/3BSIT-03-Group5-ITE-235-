using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected Transform projectilePos;
    public static float movementSpeed;
    public bool facingRight;
    [SerializeField] private GameObject fireball;
    [SerializeField] public int health;
    [SerializeField] private EdgeCollider2D SwordCollider;
    [SerializeField] private List<string> damagesources = new List<string>();
    public abstract bool IsDead{ get; }
    public bool Attack { get; set; }
    public bool TakingDamage { get; set; }
    

    public Animator myAnimator { get; private set; }
    // Start is called before the first frame update
    public virtual void Start()
    {
        facingRight = true;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public abstract IEnumerator TakeDamage();
    public abstract void Death();

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1,1,1);    
    }

    public virtual void Throw(int value)
    {
        if(facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(fireball, projectilePos.position, Quaternion.Euler(new Vector3(0,0,0)));
                tmp.GetComponent<Fireball>().Initialize(Vector2.right);
            }
        else
            {
                GameObject tmp = (GameObject)Instantiate(fireball, projectilePos.position, Quaternion.Euler(new Vector3(0,0,-180)));
                tmp.GetComponent<Fireball>().Initialize(Vector2.left);
            }
    }
    public void SwordDisable()
    {
        SwordCollider.enabled = false;
    }
    
    public void MeleeAttack()
    {
        SwordCollider.enabled = !SwordCollider.enabled;
        SwordCollider.transform.position = new Vector3(SwordCollider.transform.position.x + 0.01f, SwordCollider.transform.position.y, SwordCollider.transform.position.z);
    }
    public virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if(damagesources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }

    public static IEnumerator FlashDamage(SpriteRenderer body)
    {
        body.color = Color.red;
        yield return new WaitForSeconds(0.075f);
        body.color = Color.white;
    }
}
