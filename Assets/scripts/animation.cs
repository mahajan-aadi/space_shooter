using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    private player _player_info;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _player_info = GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player_info.single_player) { animate1(); }
        if (_player_info.coop) { animate2(); }
    }
    private void animate1()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("right", true);
            anim.SetBool("left", false);
        }
        else if ( Input.GetKeyUp(KeyCode.D)) { anim.SetBool("right", false); }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("left", true);
            anim.SetBool("right", false);
        }
        else if (Input.GetKeyUp(KeyCode.A)) { anim.SetBool("left", false); }
    }
    private void animate2()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) )
        {
            anim.SetBool("right", true);
            anim.SetBool("left", false);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow)) { anim.SetBool("right", false); }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetBool("left", true);
            anim.SetBool("right", false);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow)) { anim.SetBool("left", false); }
    }
}
