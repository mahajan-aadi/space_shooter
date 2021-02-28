using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class player : MonoBehaviour
{
    private Transform _shield;
    private float _first = 0.25f;
    private float _second = 0;
    private Hashtable _powerups = new Hashtable();
    private Hashtable _powerups2 = new Hashtable();
    [SerializeField]
    private int _lives = 3;
    private game_handler _game_manager;
    public GameObject[] engines;
    private AudioSource _laser;
    public bool single_player=false;
    [SerializeField]
    public bool coop=false;
    // Start is called before the first frame update
    void Start()
    {
        _game_manager = GameObject.Find("game_manager").GetComponent<game_handler>();
        _shield = transform.FindChild("Shield");
        _powerups["triple"] = false;
        _powerups["speed"] = false;
        _powerups["shield"] = false;
        _powerups2["triple"] = false;
        _powerups2["speed"] = false;
        _powerups2["shield"] = false;
        _game_manager.change_lives(_lives,single_player);
        _laser = GetComponent < AudioSource >();
    }

    // Update is called once per frame
    void Update()
    {
        if (single_player)
        {
            movement();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                shoot();
            }
        }
        if(coop)
        {
            movement();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                shoot();
            }

        }
    }

    private void shoot()
    {
        if (_second+ Time.time > _first)
        { 
            if (!(bool)_powerups["triple"]  &&  !(bool)_powerups2["triple"])
            {
                _laser.Play();
                GameObject Laser= (GameObject)Instantiate(Resources.Load("laser", typeof(GameObject)), transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
                laser value = Laser.GetComponent<laser>();
                if (coop) { value.single = false; }
            }
            else
            {
                _laser.Play();
                GameObject Laser= (GameObject)Instantiate(Resources.Load("triple_shot", typeof(GameObject)), transform.position + new Vector3(0.5f, 0.3f, -11), Quaternion.identity);
                laser [] Lasers = Laser.GetComponentsInChildren<laser>();
                foreach(laser value in Lasers) { if (coop) { value.single = false; } }
            }
            _first = Time.time+0.25f;
        }
    }
    private void movement()
    {
        float horizontal_movement=0;
        float vertical_movement=0;
        if (single_player)
        {
            horizontal_movement = Input.GetAxis("Horizontal");
            vertical_movement = Input.GetAxis("Vertical");
        }
        if (coop)
        {
            if (horizontal_movement == 0) { horizontal_movement = Input.GetAxis("Horizontal2"); }
            if (vertical_movement == 0) { vertical_movement = Input.GetAxis("Vertical2"); }
        }
        if ((bool)_powerups["speed"]|| (bool)_powerups2["speed"])
        {
            horizontal_movement *= 2;
            vertical_movement *= 2;
        }
        transform.Translate(Vector3.right * horizontal_movement * Time.deltaTime * 8);
        transform.Translate(Vector3.up * vertical_movement * Time.deltaTime * 8);
        //float x_axis = Camera.main.pixelRect.width * Camera.main.aspect / 100 - 0 * Camera.main.aspect/100;
        //float y_axis = Camera.main.pixelRect.height * Camera.main.aspect / 100 - 0 * Camera.main.aspect/100;
        if (-8 > transform.position.x)
        {
            transform.position = new Vector3(-8,transform.position.y,0);
        }
        else if (transform.position.x > 8)
        {
            transform.position = new Vector3(8, transform.position.y, 0);
        }
        else if (-4 > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
        else if(transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, 0);
        }
    }
    public void powerup_enter(string powerup)
    {
        if ((bool)_powerups[powerup]) { StartCoroutine(wait_time2(powerup)); }
        else { StartCoroutine(wait_time(powerup)); }
    }
    IEnumerator wait_time(string powerup)
    {
        _powerups[powerup] = true;
        if (powerup == "shield"){ _shield.gameObject.SetActive(true); }
        yield return new WaitForSeconds(5.0f);
        if (powerup == "shield" && !(bool)_powerups2[powerup]) { _shield.gameObject.SetActive(false); }
        _powerups[powerup] = false;
    }
    IEnumerator wait_time2(string powerup)
    {
        _powerups2[powerup] = true;
        if (powerup == "shield") { _shield.gameObject.SetActive(true); }
        yield return new WaitForSeconds(5.0f);
        if (powerup == "shield" && !(bool)_powerups[powerup]) { _shield.gameObject.SetActive(false); }
        _powerups2[powerup] = false;
    }
    public void destroy_player()
    {
        if ((bool)_powerups["shield"] || (bool)_powerups2["shield"])
        {
            _powerups["shield"] = false;
            _powerups2["shield"] = false;
            _shield.gameObject.SetActive(false);
            return;
        }
        _lives--;
        if (_lives == 2)
        {
            int failure = Random.Range(0, engines.Length);
            engines[failure].SetActive(true);
        }
        if (_lives == 1)
        {
            foreach(GameObject engine in engines)
            {
                engine.SetActive(true); 
            }
        }
        _game_manager.change_lives(_lives,single_player);
        if (_lives == 0)
        {
            if (!_game_manager.coop)
            {
                _game_manager.menu.SetActive(true);
                _game_manager.gameover = true;
            }
            else if (transform.parent.childCount==1)
            {
                _game_manager.menu.SetActive(true);
                _game_manager.gameover = true;
            }
            Instantiate(Resources.Load("player_destroy", typeof(GameObject)), transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
