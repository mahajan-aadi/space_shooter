using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class game_handler : MonoBehaviour
{
    private Image _lives1, _lives2;
    private Text _scoretext1, _scoretext2,_highscoretext;
    private int _score1,_score2,_highscore;
    public bool gameover = true;
    public GameObject menu,pause_menu;
    private spawn_manager _spawn;
    [SerializeField]
    private AudioClip _clip1, _clip2;
    private AudioSource _audio;
    public bool coop;
    private Animator _pause_animation;
    // Start is called before the first frame update
    void Start()
    {
        _highscore = PlayerPrefs.GetInt("score", 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _pause_animation = pause_menu.GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _lives1 = transform.FindChild("lives1").GetComponent<Image>();
        _scoretext1 = transform.FindChild("score1").GetComponent<Text>();
        _spawn = GameObject.FindObjectOfType<spawn_manager>();
        _pause_animation.updateMode = AnimatorUpdateMode.UnscaledTime;
        if (coop)
        {
            _lives2 = transform.FindChild("lives2").GetComponent<Image>();
            _scoretext2 = transform.FindChild("score2").GetComponent<Text>();
        }
        else { _highscoretext = transform.FindChild("highscore").GetComponent<Text>(); }
    }

    // Update is called once per frame
    void Update()
    {
        start_screen();
    }
    private void start_screen()
    {
        if (gameover)
        {
            if (!coop) { highscore(); }
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("main_menu"); 
            }
            if (_audio.clip == _clip2)
            {
                _spawn.StopAllCoroutines();
                _audio.Stop();
                _audio.clip = _clip1;
                _audio.Play();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {

                _audio.Stop();
                _audio.clip = _clip2;
                _audio.Play();
                _score1 = 0;
                _score2 = 0;
                menu.SetActive(false);
                gameover = false;
                _spawn.start_spawn();
                update_score(true);
                if (!coop)
                {
                    Instantiate(Resources.Load("Player", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity);
                }
                else
                {
                    update_score(false);
                    Instantiate(Resources.Load("co_op", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P)) { pause(); }
        }
    }
    public void change_lives(int life,bool value)
    {
        if (value)
        {
            _lives1.sprite = (Sprite)Resources.Load("lives-" + life.ToString(), typeof(Sprite));
            //print(Resources.Load("lives-" + life.ToString(), typeof(Sprite)));
        }
        else 
        {
            _lives2.sprite = (Sprite)Resources.Load("lives-" + life.ToString(), typeof(Sprite));
        }
    }
    public void update_score(bool value)
    {
        if (value)
        {
            _scoretext1.text = "Score:" + _score1.ToString();
            _score1 += 10;
        }
        else
        {
            _scoretext2.text = "Score:" + _score2.ToString();
            _score2 += 10;
        }
    }
    private void highscore()
    {
        if (_score1 > _highscore) 
        {
            _highscore = _score1;
            _highscoretext.text = "Highscore:" + _highscore.ToString();
            PlayerPrefs.SetInt("score", _highscore);
        }
    } 
    public void main_menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("main_menu");
    }
    public void resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        _pause_animation.SetBool("pause_entry",false);
        _pause_animation.SetBool("pause_exit", true);
    }
    public void pause()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _pause_animation.SetBool("pause_entry", true);
        _pause_animation.SetBool("pause_exit", false);
    }
}
