using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoTrail : MonoBehaviour
{
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public GameObject echoRun;
    public GameObject echoJump;
    public GameObject echoSlide;
    // private Player player;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Player.Instance.Artif3Active == true && Player.Instance.MyRigidbody.velocity.x != 0)
        {
            if(Player.Instance.facingRight == true)
            {
                if(Player.Instance.OnGround == true)
                {
                    if(Player.Instance.Slide == true)
                    {
                        Spawn(echoSlide, new Vector3(0,0,0));
                    }else
                    {
                        Spawn(echoRun, new Vector3(0,0,0));
                    }
                
                }
                else
                {
                    Spawn(echoJump, new Vector3(0,0,0));
                }
            }else
            {
                if(Player.Instance.OnGround == true)
                {
                    if(Player.Instance.Slide == true)
                    {
                        Spawn(echoSlide, new Vector3(0,180,0));
                    }else
                    {   
                        Spawn(echoRun, new Vector3(0,180,0));
                    }
                }else
                {
                    Spawn(echoJump, new Vector3(0,180,0));
                }

            }
        }
    }

    void Spawn(GameObject x,Vector3 y)
    {  if(timeBtwSpawns<=0)
        {
            GameObject instance = (GameObject)Instantiate(x,transform.position,Quaternion.Euler(y));
            Destroy(instance, 4f);
            timeBtwSpawns=startTimeBtwSpawns;
                
        }else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
