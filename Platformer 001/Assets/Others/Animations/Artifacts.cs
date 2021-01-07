using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifacts : MonoBehaviour
{
    public int artifactNum;
    public GameObject explosionParticles;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.audioManager.Play("ArtifactCollect");
            if(this.artifactNum == 1)
            {
                Player.Instance.ArtifactImagePickUp.sprite = Player.Instance.artifactImage1.sprite;
                Player.Instance.ArtifactTitlePickUp.text = "Artifact of Earth";
                Player.Instance.ArtifactDescPickUp.text = "HEALS THE PLAYER FOR 30% OF HIS MAX HP UPON USE.\nCooldown : 15";
                Player.Instance.hasArtif1 = true;
                Player.Instance.artifactImage1.fillAmount = 0;
                Player.Instance.artifCount += 1;
                Player.Instance.cooldownText.text = "U";
                Player.Instance.cooldownText.fontSize = 106;
                Player.Instance.cooldownText.gameObject.SetActive(true);
                Player.Instance.PickUpUI.SetActive(true);

                
            }else if(this.artifactNum == 2)
            {
                Player.Instance.ArtifactImagePickUp.sprite = Player.Instance.artifactImage2.sprite;
                Player.Instance.ArtifactTitlePickUp.text = "Artifact of Air";
                Player.Instance.ArtifactDescPickUp.text = "INCREASES HEIGHT OF PLAYER'S JUMP UPON USE.\nDuration : 6\nCooldown : 20";
                Player.Instance.hasArtif2 = true;
                Player.Instance.artifactImage2.fillAmount = 0;
                Player.Instance.artifCount += 1;
                Player.Instance.cooldownText2.text = "I";
                Player.Instance.cooldownText2.fontSize = 106;
                Player.Instance.cooldownText2.gameObject.SetActive(true);
                Player.Instance.PickUpUI.SetActive(true);

            }else if(this.artifactNum == 3)
            {
                Player.Instance.ArtifactImagePickUp.sprite = Player.Instance.artifactImage3.sprite;
                Player.Instance.ArtifactTitlePickUp.text = "Artifact of Spirit";
                Player.Instance.ArtifactDescPickUp.text = "MAKES THE PLAYER INVINCIBLE UPON USE.\nDuration : 5\nCooldown : 25";
                Player.Instance.hasArtif3 = true;
                Player.Instance.artifactImage3.fillAmount = 0;
                Player.Instance.artifCount += 1;
                Player.Instance.cooldownText3.text = "O";
                Player.Instance.cooldownText3.fontSize = 106;
                Player.Instance.cooldownText3.gameObject.SetActive(true);
                Player.Instance.PickUpUI.SetActive(true);
            }else if(this.artifactNum == 4)
            {
                Player.Instance.Win();

                Player.Instance.hasArtif1 = false;
                Player.Instance.hasArtif2 = false;
                Player.Instance.hasArtif3  = false;
                Player.Instance.durationCount3 = 1;
                Player.Instance.durationCount2= 0;
                Player.Instance.isCooldown = false;
                Player.Instance.isCooldown2 = false;
                Player.Instance.isCooldown3 = false;

                Player.Instance.immortal = true;
                Player.Instance.isCooldown2 = false;
                Player.Instance.cooldownText.gameObject.SetActive(false);
                Player.Instance.cooldownText2.gameObject.SetActive(false);
                Player.Instance.cooldownText3.gameObject.SetActive(false);
                Player.Instance.artifactImage1.fillAmount = 1;
                Player.Instance.artifactImage2.fillAmount = 1;
                Player.Instance.artifactImage3.fillAmount = 1;
                

                Instantiate(Player.Instance.anvilParticlesWIN,Player.Instance.anvilPos.position,Quaternion.identity);
            }
        Instantiate(explosionParticles,this.transform.position,Quaternion.identity);
        Destroy(gameObject);  
            

        }
         
    }

}
