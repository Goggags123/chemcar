using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Input_Field : MonoBehaviour
{
    public Text error;
    private InputField inputF;
    //public Data data;
    // Start is called before the first frame update
    void Start()
    {
        inputF = GetComponent<InputField>();
    }
    public void nextScene()
    {
        if (inputF.text != "")
        {
            error.text = "";
            Data.user_name = inputF.text;
            SceneManager.LoadScene(1);
        }
        else
        {
            error.text = "!!!Please enter your name!!!";
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
