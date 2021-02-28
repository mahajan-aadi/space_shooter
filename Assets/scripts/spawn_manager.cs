using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_manager : MonoBehaviour
{
    private List<Object> _powerups=new List<Object>();
    private List<Object> _enemy = new List<Object>();
    private game_handler _game_manager;
    // Start is called before the first frame update
    void Start()
    {
        _game_manager = GameObject.FindObjectOfType<game_handler>();
        _powerups.Add(Resources.Load("speed",typeof(GameObject)));
        _powerups.Add(Resources.Load("triple", typeof(GameObject)));
        _powerups.Add(Resources.Load("shield", typeof(GameObject)));
        _enemy.Add(Resources.Load("Enemy_straight", typeof(GameObject)));

    }
    public void start_spawn() 
    {
        StartCoroutine(enemy_start());
        StartCoroutine(powerup_start());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator enemy_start()
    {
        while (!_game_manager.gameover)
        {
            float x = Random.Range(-7.4f, 7.4f);
            int enemy = Random.Range(0, _enemy.Count);
            Instantiate(_enemy[enemy], new Vector3(x, 6.2f, 0), Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }
    IEnumerator powerup_start()
    {
        while (!_game_manager.gameover)
        {
            float x = Random.Range(-8.2f, 8.2f);
            int powerups= Random.Range(0, _powerups.Count);
            Instantiate(_powerups[powerups], new Vector3(x, 4.4f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
