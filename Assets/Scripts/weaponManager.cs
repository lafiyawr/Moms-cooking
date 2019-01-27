using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour

{
    public float weaponSpeed;
    public float weaponDamage;
    public float weaponDeath;
    public Rigidbody2D weaponsRB;
    //public GameObject sparks;
    public AudioSource weaponPlayer;
    public SpriteRenderer sRen;

    public AudioClip weaponSound;
    [Range(0.0f, 1.0f)]
    public float weaponVolume;
    // Start is called before the first frame update
    void Start()
    {
        weaponsRB.velocity = transform.right*-1 * weaponSpeed;
        Destroy(gameObject, weaponDeath);
       // sparks.gameObject.SetActive(true);
        this.sRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {

        if(other.gameObject.tag != "Player")
        {
            StartCoroutine(delayThrow()); 
        }




    }

    IEnumerator delayThrow()
    {
        weaponPlayer.PlayOneShot(weaponSound, weaponVolume);
        // Destroy(gameObject);  
       // var sparksCopy = Instantiate(sparks, transform.position, Quaternion.identity);
        sRen.enabled = false;
        yield return new WaitForSeconds(0.1f);
         //Destroy(sparksCopy);
        gameObject.SetActive(false);
       

    }
}
