using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public int data;
    private int[] scores = new int[6];
    private string[] names = new string[6];
    public bool isWriting;
    public Text first;
    public Text second;
    public Text third;
    public Text fourth;
    public Text fifth;
    public Text score1;
    public Text score2;
    public Text score3;
    public Text score4;
    public Text score5;
    public string user;
    public static int score = 0;
    private string path;
    public Text current_score;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/Scoreboard.txt";
        if (File.Exists(path))
        {
            if (isWriting)
            {
                WriteData();
            }
            else
            {
                readData();
                first.text = names[0];
                second.text = names[1];
                third.text = names[2];
                fourth.text = names[3];
                fifth.text = names[4];
                score1.text = scores[0].ToString();
                score2.text = scores[1].ToString();
                score3.text = scores[2].ToString();
                score4.text = scores[3].ToString();
                score5.text = scores[4].ToString();
                current_score.text = score.ToString();
            }
        }
    }
    public void WriteData()
    {
        readData();
        StreamWriter writer = new StreamWriter(path, false);
        scores[5] = data;
        names[5] = user;
        for (int i = 0; i < 6; i++)
        {
            for (int j = i+1; j < 6; j++)
            {
                if (scores[i] <= scores[j])
                {
                    int temp = scores[j];
                    scores[j] = scores[i];
                    scores[i] = temp;
                    string tempo = names[j];
                    names[j] = names[i];
                    names[i] = tempo;
                }
            }
        }
        for (int i = 0; i < 6; i++)
        {
            writer.WriteLine(names[i]);
            writer.WriteLine(scores[i]);
        }
        writer.Close();
    }
    public void readData()
    {
        StreamReader reader = new StreamReader(path);
        for (int i = 0; i < 6; i++)
        {
            names[i] = reader.ReadLine();
            scores[i] = int.Parse(reader.ReadLine());
        }
        reader.Close();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
