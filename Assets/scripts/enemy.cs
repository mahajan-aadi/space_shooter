using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private game_handler game_manager;
    // Start is called before the first frame update
    void Start()
    {
        game_manager = GameObject.Find("game_manager").GetComponent<game_handler>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * 10 * Time.deltaTime);
        if (transform.position.y < -6.4)
        {
            float x = Random.Range(-7.4f, 7.4f);
            transform.position = new Vector3(x,6.4f,0);
        }
        if (game_manager.gameover)
        {
            Destroy(this.gameObject);
            Instantiate(Resources.Load("enemy_destroy", typeof(GameObject)), transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player Player = collision.GetComponent<player>();
            Destroy(this.gameObject);
            Instantiate(Resources.Load("enemy_destroy", typeof(GameObject)), transform.position, Quaternion.identity);
            Player.destroy_player();
            game_manager.update_score(Player.single_player);
        }
    }
}
