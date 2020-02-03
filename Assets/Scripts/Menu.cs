using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    public TMP_InputField widthField, heightField;
    public TextMeshProUGUI annotation;
    public GameObject menuObj;
    float minSize = 10, maxSize = 100;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Play()
    {
        float width = 0, height = 0;
        if (!float.TryParse(widthField.text, out width) || !float.TryParse(heightField.text, out height))
        {
            IncorrectInput();
            return;
        }
        if (width < minSize || width > maxSize || height < minSize || height > maxSize)
        {
            IncorrectInput();
            return;
        }
        if (width / height > 2 || height / width > 2)
        {
            IncorrectInput();
            return;
        }

        GameManager.Instance.field.SetBorder(width, height);
        menuObj.SetActive(false);
    }

    void IncorrectInput()
    {
        annotation.color = Color.red;
        annotation.SmoothlyColor(Color.white, 1, 1);
    }
}
