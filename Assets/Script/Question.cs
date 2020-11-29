using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Question : MonoBehaviour
{
    public AudioClip Crank_sound;
    public AudioClip Correct_sound;
    public int d;
    public AudioClip Incorrect_sound;
    public AudioSource Audio1;
    public AudioSource Button1;
    public AudioSource Button2;
    public Image question;
    public Image Health_bar;
    private float health = 150f;
    private float clock = 0;
    private float count_time = 0;
    private float display_time = 0;
    private bool isClicked = false;
    private bool once = true;
    private bool one_time = true;
    public Button A;
    public Button B;
    public Button C;
    public Button D;
    public List<Sprite> questions = new List<Sprite>();
    public List<Sprite> answers = new List<Sprite>();
    public Car player;
    private int[] correct_choice = { 4,1,2,1,4,2,4,4,3,4,2,1,1,3,1};
    public int count=0;
    private int current_choice = 0;
    private Color default_color;

    private void Start()
    {
        Health_bar.color = Color.green;
        Button1.clip = Correct_sound;
        Button2.clip = Incorrect_sound;
        Audio1.clip = Crank_sound;
        Shuffle(questions, answers,correct_choice);
        default_color = A.image.color;
        Stop_Display();
    }
    public void Display()
    {
        isClicked = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        question.enabled = true;
        A.image.enabled = true;
        B.image.enabled = true;
        C.image.enabled = true;
        D.image.enabled = true;
        A.interactable = true;
        B.interactable = true;
        C.interactable = true;
        D.interactable = true;
        A.image.color = default_color;
        B.image.color = default_color;
        C.image.color = default_color;
        D.image.color = default_color;
        question.sprite = questions[count];
        A.image.sprite = answers[4*count];
        B.image.sprite = answers[4*count + 1];
        C.image.sprite = answers[4*count + 2];
        D.image.sprite = answers[4*count + 3];
    }
    public void Stop_Display()
    {
        player.GetComponent<Rigidbody>().isKinematic = false;
        question.enabled = false;
        A.image.enabled = false;
        B.image.enabled = false;
        C.image.enabled = false;
        D.image.enabled = false;
        current_choice = 0;
    }
    public void setOne()
    {
        current_choice = 1;
    }
    public void setTwo()
    {
        current_choice = 2;
    }
    public void setThree()
    {
        current_choice = 3;
    }
    public void setFour()
    {
        current_choice = 4;
    }
    public void Button_Clicked()
    {
        A.interactable = false;
        B.interactable = false;
        C.interactable = false;
        D.interactable = false;
        isClicked = true;
        Color green = Color.green;
        Color red = Color.red;
        red.a = 0.8f;
        if (current_choice==correct_choice[count])
        {
            Button1.Play();
            count_time -= 10f;
            player.score++;            
        }
        else
        {
            Button2.Play();
            count_time += 20f;
        }
        if (correct_choice[count] == 1)
        {
            A.image.color = green;
            B.image.color = red;
            C.image.color = red;
            D.image.color = red;
        }
        else if (correct_choice[count] == 2)
        {
            A.image.color = red;
            B.image.color = green;
            C.image.color = red;
            D.image.color = red;
        }
        else if (correct_choice[count] == 3)
        {
            A.image.color = red;
            B.image.color = red;
            C.image.color = green;
            D.image.color = red;
        }
        else if (correct_choice[count] == 4)
        {
            A.image.color = red;
            B.image.color = red;
            C.image.color = red;
            D.image.color = green;
        }
        count++;
        display_time = 0f;
    }
    void Shuffle(List<Sprite> q, List<Sprite> a,int[] c)
    {
        int n = q.Count;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            int tempo = c[n];
            c[n] = c[k];
            c[k] = tempo; 
            Sprite temp = q[n];
            q[n] = q[k];
            q[k] = temp;
            for(int i =0;i<4;i++)
            {
                temp = a[k * 4+i];
                a[k * 4+i] = a[n * 4+i];
                a[n * 4+i] = temp;
            }
        }
    }
    private void Update()
    {
        clock += Time.deltaTime;
        if(clock>=15 && once)
        {
            Audio1.Play();
            Display();
            once = false;
        }
        else if(clock < 15)
        {
            count_time += Time.deltaTime;
            health = 150f-count_time;
        }
        if (isClicked)
        {
            display_time += Time.deltaTime;
        }
        if (display_time >= 2f)
        {
            display_time = 0;
            clock = 0;
            once = true;
            isClicked = false;
            Stop_Display();
        }
        if (health <= 50f)
        {
            Health_bar.color = Color.red;
            if (one_time)
            {
                player.Smoke.Play();
                one_time = false;
            }
        }
        else
        {
            one_time = true;
            Health_bar.color = Color.green;
            player.Smoke.Stop();
        }
        if(count>=10&&display_time==0f)
        {
            EndGame();
        }
        if (health>0f)
        {
            Health_bar.fillAmount = health/150f;
        }
        else
        {
            EndGame();
        }
    }
    private void EndGame()
    {
        GetComponent<Scoreboard>().data = player.score;
        Scoreboard.score = player.score;
        GetComponent<Scoreboard>().user = Data.user_name;
        GetComponent<Scoreboard>().WriteData();
        SceneManager.LoadScene(d);
    }
}