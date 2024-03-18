using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    public ParticleSystem rewardParticles;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnParts(GameObject baseObj, GameObject reward)
    {
        ParticleSystem grabParticles = Instantiate(rewardParticles, baseObj.transform.position, baseObj.transform.rotation);
        grabParticles.gameObject.SetActive(true);
        grabParticles.Play();
        Debug.Log("particles should be playing");

        ParticleSystem newParticles = Instantiate(rewardParticles, reward.transform.position, reward.transform.rotation);
        newParticles.gameObject.SetActive(true);
        newParticles.Play();
    }




}
