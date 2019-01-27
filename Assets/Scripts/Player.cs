using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 1.0f;

    public GameObject weapon;
    public Transform weaponTransform;
    public int playerHealth;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
           // Debug.Log(transform.rotation.z);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * Time.deltaTime * speed;
            // Debug.Log(transform.rotation.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Instantiate(weapon, weaponTransform.position, weaponTransform.rotation);

        }


    }


}
