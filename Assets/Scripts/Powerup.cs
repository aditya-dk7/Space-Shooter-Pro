using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerUpSpeed = 5.0f;
    [SerializeField]
    private int powerupID;
    // Start is called before the first frame update
    [SerializeField]
    private AudioClip _clip;
 

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _powerUpSpeed);


        if(transform.position.y < -3.638f)
        {
            
            Destroy(this.gameObject);
        }

       

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();


            AudioSource.PlayClipAtPoint(_clip,transform.position);
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedPowerupActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("Default");
                        break;

                }
                
            }
            Destroy(this.gameObject);
        }
    }
}
