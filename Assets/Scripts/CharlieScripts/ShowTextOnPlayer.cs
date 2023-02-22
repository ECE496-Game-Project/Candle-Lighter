using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class ShowTextOnPlayer : MonoBehaviour
{
    public GameObject textBox;
    public List<string> textLines;
    private string file_root_path;
    public string filename;

    private void Awake()
    {
        file_root_path = Application.dataPath + "/Text/";
        readTextFile(file_root_path + filename + ".txt");
        textBox = (GameObject.Find("TextBox") != null) ? GameObject.Find("TextBox") : textBox;
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
            Debug.Log($"file path: {file_path} of object {gameObject} could not be found, text is missing.");
        }    
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!textBox.GetComponent<TextBoxViewer>().isLocked && other.CompareTag("Player"))
    //    {
    //        Debug.Log("Enter");
    //        textBox.GetComponent<TextBoxViewer>().isLocked = true;
    //        textBox.GetComponent<TextBoxViewer>().OpenTextBox(textLines);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (textBox.GetComponent<TextBoxViewer>().isLocked && other.CompareTag("Player"))
    //    {
    //        textBox.GetComponent<TextBoxViewer>().isLocked = false;
    //        textBox.GetComponent<TextBoxViewer>().CloseTextBox();
    //    }
    //}
}
