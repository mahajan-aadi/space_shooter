using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_up : MonoBehaviour
{
    [SerializeField]
    private int _index;
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 3);
        if (transform.position.y < -5.7f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player Player = collision.GetComponent<player>();
            if (_index == 0){Player.powerup_enter("triple");}
            else if (_index == 1){Player.powerup_enter("speed");}
            else if (_index == 2){Player.powerup_enter("shield");}
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 0.1f);
            Destroy(this.gameObject);
        }
    }
}
