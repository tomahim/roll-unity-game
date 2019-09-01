using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{   
    public AudioSource pickupSound;
    public bool isCatched = false;
    public bool isCounted = false;

     private void OnTriggerEnter(Collider other) {                           
        if (other.gameObject.CompareTag("Player") && !isCatched) {
            isCatched = true;                         
            pickupSound.Play();                                             
            StartCoroutine(makeGemDisappear());                                      
        }                                                                   
    }                                                                       
                                                                            
    private IEnumerator makeGemDisappear() {
        ParticleSystem particle = transform.GetComponent<ParticleSystem>(); 
        particle.Play();                                                    
        transform.Find("Cones").gameObject.SetActive(false);                
        yield return new WaitForSeconds(0.55f);                                
        particle.Stop();                                                    
        Destroy(transform.gameObject);                                      
    }                                                                       
}
