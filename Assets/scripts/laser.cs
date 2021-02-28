using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private game_handler game_manager;
    public bool single=true;
    // Start is called before the first frame update
    void Start()
    {
        game_manager = GameObject.Find("game_manager").GetComponent<game_handler>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * 10 * Time.deltaTime);
        if (transform.position.y > 5.6)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            Instantiate(Resources.Load("enemy_destroy", typeof(GameObject)), transform.position, Quaternion.identity);
            game_manager.update_score(single);
        }
    }
}
