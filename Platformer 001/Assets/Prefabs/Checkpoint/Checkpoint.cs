using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    private Collider2D flagCollider;
    private Animator flagAnimator;
    private Vector3 flagPos;
    [SerializeField] private GameObject flagParticles;
    
    private void Awake() 
    {
        flagCollider = GetComponent<Collider2D>();
        flagAnimator = GetComponent<Animator>();
        flagPos = flagCollider.transform.position;
    }
    
        
    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(!Player.Instance.IsDead && Player.Instance.health>0)
            {
                AudioManager.audioManager.Play("CheckpointSound");
                flagAnimator.SetTrigger("ActivateCheckpoint");
                flagCollider.enabled = false;
                Player.startPos = flagPos;

            }
            
        }
        
    }

    void ParticleStart()
    {
        Instantiate(flagParticles,gameObject.transform.position,Quaternion.identity);
    }
}
