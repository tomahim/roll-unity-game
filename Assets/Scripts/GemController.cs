using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{   
    public AudioSource pickupSound;

     private void OnTriggerEnter(Collider other) {                           
        if (other.gameObject.CompareTag("Player")) {                        
            pickupSound.Play();                                             
            StartCoroutine(makeGemDisappear());                                      
        }                                                                   
    }                                                                       
                                                                            
    private IEnumerator makeGemDisappear() {
        Debug.Log("disappear");                                
        ParticleSystem particle = transform.GetComponent<ParticleSystem>(); 
        particle.Play();                                                    
        transform.Find("Cones").gameObject.SetActive(false);                
        yield return new WaitForSeconds(0.8f);                                
        particle.Stop();                                                    
        Destroy(transform.gameObject);                                      
    }                                                                       
}
