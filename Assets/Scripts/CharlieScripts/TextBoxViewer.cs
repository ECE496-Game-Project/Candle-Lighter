using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxViewer : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject textBox;
    public bool isLocked;

    private int index;

    void Start()
    {
        isLocked = false;
        textBox = transform.GetChild(0).gameObject;
        textBox.SetActive(false);
        textComponent.text = string.Empty;
    }

    public void OpenTextBox(List<string> lines)
    {
        Debug.Log("first line: " + lines[0]);
        textBox.SetActive(true);
        Debug.Log($"textBox:{textBox}");
        index = 0;

        while (index < lines.Count)
        {
            textComponent.text += lines[index];
            textComponent.text += '\n';
            index++;
        }
    }

    public void CloseTextBox()
    {
        textComponent.text = string.Empty;
        textBox.SetActive(false);
    }
}
