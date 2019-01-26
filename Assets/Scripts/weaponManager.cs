using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour

{
    public float weaponSpeed;
    public float weaponDamage;
    public float weaponDeath;
    public Rigidbody2D weaponsRB;
    // Start is called before the first frame update
    void Start()
    {
        weaponsRB.velocity = transform.right*-1 * weaponSpeed;
        Destroy(gameObject, weaponDeath);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D()
    {
        Debug.Log("hit!");
        Destroy(gameObject);  
    }
}
