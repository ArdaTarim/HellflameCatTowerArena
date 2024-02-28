using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KillPlayer : MonoBehaviour
{

    public int respawn;    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene(respawn);
        }
    }

    public void killPlayer(RaycastHit hit)
    {
        if(hit.collider.CompareTag("Player"))
        {
            SceneManager.LoadScene(respawn);
        }
    }
}
