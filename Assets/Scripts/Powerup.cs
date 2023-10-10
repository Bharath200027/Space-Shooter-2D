using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private float powerupID;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y > 3.7f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject); ;
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                switch(powerupID)
                {
                case 0:
                    player.TripleShotActive();
                    break;
                case 1:
                    player.ShieldsActive();
                    break;
                default:
                    Debug.Log("Default Value");
                    break;
                }
            }
            Destroy(this.gameObject);
        }
    }

}
