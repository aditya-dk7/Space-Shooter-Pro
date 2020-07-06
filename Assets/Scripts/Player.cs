using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

     

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0); 
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        // It is similar to write Vector3(1,0,0) * Time * 5 -> Makes it go 5m/sec
        //transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        //transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);

        //The instructor made me realize that I was using two new vectors to ddo the same movement
        //Thefore, the above is commented out and I have a new method to approach it
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"),
             Input.GetAxis("Vertical"), 0) * speed * Time.deltaTime);
        if (transform.position.y >= 2)
        {
            transform.position = new Vector3(transform.position.x, 2, 0);
        }
        else if (transform.position.y <= -2f)
        {
            transform.position = new Vector3(transform.position.x, -2f, 0);
        }

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }
}
