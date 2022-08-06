using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraft : MonoBehaviour
{
    [SerializeField] GameObject laserShotPF;
    protected bool isInvincible = false;
    protected float invincibilityDuration = 1.0f;                     //how long is player invincible after getting hit/ enemy after spawn
    protected Vector3 laserOffset = new Vector3(0.5f, 0, 0);

    protected IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        //set animation here or in update
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
    protected void FireLaser()
    {
        Instantiate(laserShotPF, gameObject.transform.position - laserOffset, laserShotPF.transform.rotation);
        Instantiate(laserShotPF, gameObject.transform.position + laserOffset, laserShotPF.transform.rotation);
    }
    protected virtual void Death()
    {
        //Play death/gameover noise

    }
}
