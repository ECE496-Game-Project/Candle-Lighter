using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxViewer : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public bool isLocked = false;

    private int index;

    void Start()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }

    public void OpenTextBox(List<string> lines)
    {
        gameObject.SetActive(true);
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
        gameObject.SetActive(false);
    }
}
