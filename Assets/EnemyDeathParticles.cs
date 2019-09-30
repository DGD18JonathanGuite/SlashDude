using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathParticles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyParticles());
    }

    IEnumerator DestroyParticles()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public IEnumerator MoveTowardsTarget(GameObject _target)
    {
        Debug.Log("MoveTowardsNode");
        yield return new WaitForSeconds(0.2f);
        ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[1000];

        ParticleSystem _particlesystem = GetComponent<ParticleSystem>();

        int length = _particlesystem.GetParticles(_particles);

        while (_particles[0].remainingLifetime > 0)
        {
            Debug.Log("Lifetime");
            for (int i = 0; i < length; i++)
            {
                _particles[i].velocity = Vector3.zero;
                _particles[i].position = _particles[i].position + (_target.transform.position - _particles[i].position) / (_particles[i].remainingLifetime) * Time.deltaTime;
            }
        }
        _particlesystem.SetParticles(_particles, length);
    }

    //public void LateUpdate()
    //{
    //    if (particleSystem.isPlaying)
    //    {
    //        int length = _particleSystem.GetParticles(_particles);
    //        Vector3 attractorPosition = _attractorTransform.position;

    //        for (int i = 0; i < length; i++)
    //        {
    //            _particles[i].position = _particles[i].position + (attractorPosition - _particles[i].position) / (_particles[i].lifetime) * Time.deltaTime;
    //        }
    //        _particleSystem.SetParticles(_particles, length);
    //    }
    //}
}
