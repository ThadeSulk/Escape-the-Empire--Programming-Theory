using System.Collections;
using UnityEngine;

public class Smoke : MonoBehaviour
{

    private void Start()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        gameObject.GetComponent<ParticleSystem>().Play();
        StartCoroutine(Destruction());
    }

    IEnumerator Destruction()
    {
        yield return new WaitForSecondsRealtime(2);
        Destroy(gameObject);
    }
}
