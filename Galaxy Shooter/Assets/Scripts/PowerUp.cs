using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int _powerupID; 

    [SerializeField]
    private AudioClip _audioClip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        { 
            Player player = other.GetComponent<Player>();

            if(player != null)
            {

                AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
                if(_powerupID == 0)
                {
                    player.EnableTripleShot();
                }
                else if(_powerupID == 1)
                {
                    player.EnableSpeedBoost();
                }
                else if(_powerupID == 2)
                {
                    player.EnalbeShield();
                }
            }

            Destroy(this.gameObject);
        }
    }   
}
