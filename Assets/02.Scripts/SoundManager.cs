using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource doorHit;  // Door에 닿았을 때
    public AudioSource dinoDie;  // Raptor가 Destroy됐을 때
    public AudioSource gameClear; // Stage를 클리어 했을 때
    public AudioSource gameOver;  // GameOver가 됐을 때
    private void Awake()
    {
        if (instance != null)
        {
        Destroy(this.gameObject);
        }
        else
        {
        instance = this;
        }
    }
    public void DoorHitSoundPlay()
    {
    doorHit.Play();
    }
    public void DinoDieSoundPlay()
    {
    dinoDie.Play();
    }
    public void GameClearSoundPlay()
    {
    gameClear.Play();
    }
    public void GameOverSoundPlay()
    {
    gameOver.Play();
    }
}
