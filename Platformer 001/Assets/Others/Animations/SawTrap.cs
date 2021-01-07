using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    public GameObject particle;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            if(Player.Instance.immortal == false && Player.Instance.Artif3Active == false)
            {
                Player.Instance.health -= 50;
                Player.Instance.StartCoroutine(Player.Instance.TakeDamage());
                //Player.Instance.OnDeath();
                Instantiate(particle,Player.Instance.transform.position,Quaternion.identity);
            }
            
        }
    }
}
