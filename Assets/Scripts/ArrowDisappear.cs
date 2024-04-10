using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDisappear : MonoBehaviour
{
    [SerializeField] private ParticleSystem arrowDisappear = default;
    //Particle Caculate
    [SerializeField]
    float particleLifetime;
    [SerializeField]
    private float timeCount = 0f;


    private void Start()
    {
        arrowDisappear.Play();
 
    }

    // Update is called once per frame
    void Update()
    {
        ///
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = particleSystem.main;
        particleLifetime = mainModule.startLifetime.constant;
        //
        timeCount += Time.deltaTime;
        if(timeCount > particleLifetime)
        {
            Debug.Log(particleLifetime);
            Destroy(gameObject);
        }
    }
}
