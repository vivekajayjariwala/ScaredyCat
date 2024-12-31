using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f; 
    public float displayImageDuration = 1f; 
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer; 
    bool m_HasAudioPlayed;

    void OnTriggerEnter (Collider other)
    {
        // if the other Collider's GameObject (the one that entered the Trigger) is equivalent to John Lemon's GameObject
        if (other.gameObject == player)
        {
            // sets variable to show that player is at the exit 
            m_IsPlayerAtExit = true; 
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true; 
    }

    void Update() 
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }

    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        // play the audio if it hasn't been played yet and then update it 
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true; 
        }
        // determining how much time has passed since the last frame and adding it to total
        m_Timer += Time.deltaTime;   

        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if(m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else 
            {
                Application.Quit();
            }
        }
    }
    
}
