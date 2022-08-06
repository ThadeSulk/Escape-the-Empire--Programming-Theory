using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraft : MonoBehaviour                         //Parent of both PlayerController and Enemy Scripts
{
    public GameObject lazerShotPF;                              //spacecraft's own lazer
    protected string harmfulLazerTag = "EnemyLazer";            //prevent error of one's own lazer damaging spacecraft

    protected bool isInvincible = false;
    protected float invincibilityDuration = 1.0f;               //how long is spacecraft invincible after invincibility is triggered (hit for player, spawn for enemy)
    protected Vector3 lazerOffset = new Vector3(-0.5f, 0, 0);

    //public ParticleSystem deathExplosion;
    //public AudioClip deathAudio;

    protected IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        //set animation here or in update
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
    protected void FireLazer()
    {
        Instantiate(lazerShotPF, gameObject.transform.position - lazerOffset, lazerShotPF.transform.rotation);
        Instantiate(lazerShotPF, gameObject.transform.position + lazerOffset, lazerShotPF.transform.rotation);
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
        //deathExplosion.GetComponent<ParticleSystem>().Play();
        //deathAudio.LoadAudioData();
    }
}
