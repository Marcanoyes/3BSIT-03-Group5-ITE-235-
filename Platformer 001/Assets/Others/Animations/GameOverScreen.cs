using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void toMain()
    {
        SceneManager.LoadScene(0);
    }
    public void GameOverSound()
    {
        
        AudioManager.audioManager.Play("GameOver");
    }
}
