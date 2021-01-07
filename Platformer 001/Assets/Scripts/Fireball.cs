using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fireball : MonoBehaviour
{   
    [SerializeField] private GameObject bulletParticles;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D FireballBody;
    private Vector2 direction;
    private int thisCollider;
    void Start()
    {
        FireballBody.GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate() {
        FireballBody.velocity = direction * speed;
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    void Update() {
        if (thisCollider == 8)
        {
            AudioManager.audioManager.Play("Fireball");
            Instantiate(bulletParticles,gameObject.transform.position,Quaternion.identity);
            Destroy(gameObject);

        }
    }

    public void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        thisCollider =  other.GetComponent<Collider2D>().gameObject.layer;
        if(this.tag == "PlayerProjectile")
        {
            if(other.tag == "Enemy")
            {
                AudioManager.audioManager.Play("Fireball");
                Instantiate(bulletParticles,gameObject.transform.position,Quaternion.identity);
                Destroy(gameObject);
            }

        }
        else if(this.tag == "EnemyProjectile")
        {
            if(other.tag == "Player")
            {
                AudioManager.audioManager.Play("Fireball");
                Instantiate(bulletParticles,gameObject.transform.position,Quaternion.identity);
                Destroy(gameObject);
                
            }

        }

        
        
        // if(other.tag == "Enemy" || other.tag == "Player" )
        // {
        //     Destroy(gameObject);
        // }
        
        
    }
}
