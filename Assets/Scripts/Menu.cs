using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI enterCount;
    public TMP_InputField countField;
    public Slider[] percents;
    //public TMP_InputField widthField, heightField;
    //public TextMeshProUGUI annotation;
    //float minSize = 10, maxSize = 100;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Play()
    {
        //float width = 0, height = 0;
        //if (!float.TryParse(widthField.text, out width) || !float.TryParse(heightField.text, out height))
        //{
        //    IncorrectInput();
        //    return;
        //}
        //if (width < minSize || width > maxSize || height < minSize || height > maxSize)
        //{
        //    IncorrectInput();
        //    return;
        //}
        //if (width / height > 2 || height / width > 2)
        //{
        //    IncorrectInput();
        //    return;
        //}

        if (int.TryParse(countField.text, out int count))
        {
            GameManager.Instance.field.SetBorder(32, 18);
            GameManager.Instance.Play(count);
            gameObject.SetActive(false);
        }
        else
        {
            enterCount.color = Color.red;
            Simple.StartMethodDelay(() => enterCount.SmoothlyAlpha(0, .5f), 2);
        }
    }

    //void IncorrectInput()
    //{
    //    annotation.color = Color.red;
    //    annotation.SmoothlyColor(Color.white, 1, 1);
    //}

    public void ChangePercent(int index)
    {
        if (EventSystem.current.currentSelectedGameObject == percents[index].gameObject)
        {
            BuilderFigures.ChangePercent(index, percents[index].value);
            for (int i = 0; i < percents.Length; i++)
                if (i != index)
                    percents[i].value = BuilderFigures.percentBehavior[i];
        }
    }
}
