using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{

    public void PlayParticle(Vector3 position)
    {
        Debug.Log("Playin");
        transform.position = position;
        GetComponent<ParticleSystem>().Play();
    }
}
