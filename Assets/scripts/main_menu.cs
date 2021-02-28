using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
    [SerializeField]
    private Canvas _instructions;
    public GameObject[] instruction_list;
    private int _number_page = -1;
    public GameObject buttons;
    private void instruction_handler()
    {
        if (instruction_list.Length - 1 == _number_page) 
        {
            back_to_main_menu();
            return;
        }
        if (_number_page != 0)
        {
            instruction_list[_number_page - 1].SetActive(false);
            instruction_list[_number_page].SetActive(true);
        }
        else { instruction_list[_number_page].SetActive(true); }

    }
    public void next()
    {
        _number_page++;
        instruction_handler();
    }
    public void start_instructions()
    {
        buttons.SetActive(false);
        _instructions.gameObject.SetActive(true);
        foreach(GameObject i in instruction_list) { i.SetActive(false); }
        next();
    }
    public void back_to_main_menu()
    {
        buttons.SetActive(true);
        _instructions.gameObject.SetActive(false);
        _number_page = -1;
    }
    public void co_op()
    {
        SceneManager.LoadScene("co-op");
    }
    public void single()
    {
        SceneManager.LoadScene("single_player");
    }
    public void quit_game()
    {
        Application.Quit();
    }
}
