using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableWIPE : MonoBehaviour
{
    void disable()
    {
        Player.Instance.gameWipeUI.SetActive(false);
        
    }

    void disableText()
    {Player.Instance.missionReminder.SetActive(false);}

    void Pause()
    {
        Player.Instance.PauseGame();
    }

    void disablePickupText()
    {Player.Instance.PickUpUI.SetActive(false);}

    public void ReminderAudio()
    {
        AudioManager.audioManager.Play("AnvilHover");
    }

    public void GameWonSound()
    {
        AudioManager.audioManager.Play("LevelWon");
    }

    
}
