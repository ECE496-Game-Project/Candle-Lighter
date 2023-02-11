using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class ShowTextOnTrigger : MonoBehaviour
{
    public GameObject textBox;
    public List<string> textLines;
    private string file_root_path;
    public string filename;

    private void Start()
    {
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

            Debug.Log(textLines[0] + " " + textLines[1]);

            input_stm.Close();
        }
        catch
        {
            Debug.Log("file path: "+file_path+" could not be found, text is missing.");
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!textBox.GetComponent<TextBoxViewer>().isLocked && other.CompareTag("Player")) // TODO: 添加玩家发出的无指令光的判断条件
        {
            textBox.GetComponent<TextBoxViewer>().isLocked = true;
            textBox.GetComponent<TextBoxViewer>().OpenTextBox(textLines);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (textBox.GetComponent<TextBoxViewer>().isLocked && other.CompareTag("Player")) // TODO: 添加玩家发出的无指令光的判断条件
        {
            textBox.GetComponent<TextBoxViewer>().isLocked = false;
            textBox.GetComponent<TextBoxViewer>().CloseTextBox();
        }
    }
}
