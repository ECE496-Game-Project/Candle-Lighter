using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class ReadText : MonoBehaviour
{
    public GameObject textBox;
    public List<string> textLines;
    private string file_root_path;
    public string filename;
    public float textSpeed = 0.2f;

    private void Awake()
    {
        textBox = (transform.Find("TextBox") != null) ? transform.Find("TextBox")?.gameObject : textBox;
        file_root_path = Application.dataPath + "/Text/";
        readTextFile(file_root_path + filename + ".txt");
    }

    void readTextFile(string file_path)
    {
        try
        {
            StreamReader input_stm = new StreamReader(file_path);
            while (!input_stm.EndOfStream)
            {
                string inputLine = input_stm.ReadLine();
                textLines.Add(inputLine);
            }

            input_stm.Close();
        }
        catch
        {
            Debug.Log("file path: "+file_path+" could not be found, text is missing.");
        }    
    }
}
