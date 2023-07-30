using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource ambientSound;
    public AudioSource victoryMusic;

    private bool isVictoryMusicPlaying = false;

    void Start()
    {
        // Certifique-se de que as músicas estejam configuradas corretamente no Inspector
        if (ambientSound == null || victoryMusic == null)
        {
            Debug.LogError("Certifique-se de atribuir as fontes de áudio no Inspector.");
            return;
        }

        // Inicie o som ambiente em loop
        PlayAmbientSound();
    }

    public void PlayAmbientSound()
    {
        if (ambientSound != null)
        {
            ambientSound.Play();
            ambientSound.loop = true;
        }
    }

    public void PlayVictoryMusic()
    {
        if (victoryMusic != null)
        {
            // Pausar o som ambiente
            if (ambientSound.isPlaying)
            {
                ambientSound.Pause();
            }

            victoryMusic.Play();
            isVictoryMusicPlaying = true;
        }
    }
}
