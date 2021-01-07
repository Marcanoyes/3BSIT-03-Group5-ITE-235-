using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilController : MonoBehaviour
{
    public GameObject anvilBubble;
    public GameObject anvilParticles;
    // public SpriteRenderer bgBubble;
    

    private void Update() {
        if(Player.Instance.hasArtif1 == true && Player.Instance.hasArtif2 == true && Player.Instance.hasArtif3 == true && Player.Instance.killCount == Player.Instance.maxEnemies)
        {
            Player.Instance.ifCompleted = true;
            // bgBubble.color = new Color(1,1,1,0.6039215686f); 

        }else
        {
            Player.Instance.ifCompleted = false;
            // bgBubble.color = new Color(1,0,0,8156862745f);
        }
        if(Player.Instance.setAnvil == true)
            {
                anvilParticles.SetActive(false);
                anvilBubble.SetActive(false);
            }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && Player.Instance.setAnvil == false)
        {
            // AudioManager.audioManager.Play("AnvilHover");
            Player.Instance.canInteract = true;
            anvilBubble.SetActive(true);
            if(Player.Instance.ifCompleted)
            {
                anvilParticles.SetActive(true);
                Player.Instance.newMissionUI1.text = "✓";
                Player.Instance.newMissionUI1.color = Color.green;
                Player.Instance.newMissionUI1Image.color = Color.green;
                Player.Instance.StartCoroutine(Player.Instance.IndicateLastMission());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player") && Player.Instance.setAnvil == false)
        {
            Player.Instance.canInteract = false;
            anvilBubble.SetActive(false);
            if(Player.Instance.ifCompleted)
            {
                anvilParticles.SetActive(false);
            }
        }
        
    }
}
