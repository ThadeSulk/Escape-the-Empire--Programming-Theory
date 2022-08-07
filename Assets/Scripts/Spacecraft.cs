using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraft : MonoBehaviour
{
    [SerializeField] GameObject laserShotPF;
    [SerializeField] GameObject deathParticles;
    public AudioSource spacecraftAudioSource;
    [SerializeField] AudioClip laserAudio;
    [SerializeField] AudioClip deathAudio;
    protected bool isInvincible = false;
    protected float invincibilityDuration = 1.0f;                     //how long is player invincible after getting hit/ enemy after spawn
    protected Vector3 laserOffset = new Vector3(0.5f, 0, 0);
    
    //Set variable for invicibility indicator
    protected Transform invincibilityShield;

    protected virtual void Awake()
    {
        invincibilityShield = transform.GetChild(0);
    }

    protected virtual IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        invincibilityShield.gameObject.SetActive(true);
        yield return new WaitForSeconds(invincibilityDuration);
        invincibilityShield.gameObject.SetActive(false);
        isInvincible = false;
    }
    protected void FireLaser()
    {
        Instantiate(laserShotPF, gameObject.transform.position - laserOffset, laserShotPF.transform.rotation);
        Instantiate(laserShotPF, gameObject.transform.position + laserOffset, laserShotPF.transform.rotation);
        spacecraftAudioSource.PlayOneShot(laserAudio, 0.5f);
    }
    protected virtual void Death()
    {
        //Play death/gameover noise
        Instantiate(deathParticles, gameObject.transform.position, deathParticles.transform.rotation);
        spacecraftAudioSource.PlayOneShot(deathAudio, 0.5f);
    }
}
