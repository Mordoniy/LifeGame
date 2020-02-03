﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider timeScaleSlider;
    public List<Figure> figures;
    public Field field;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        BuilderFigures.OnFigureCreate += OnCreateFigure;
        Figure.OnFigureCollision += OnFigureCollision;
    }

    void Update()
    {

    }

    void OnCreateFigure(Figure figure)
    {
        figures.Add(figure);
    }

    void OnFigureCollision(Figure a, Figure b)
    {
        if (figures.Contains(a) && figures.Contains(b))
        {
            figures.Remove(a);
            figures.Remove(b);
            BuilderFigures.CreateFigure(a.angleCount + b.angleCount, (a.transform.position + b.transform.position) / 2);
        }
    }

    public void Play()
    {

    }

    public Figure GetNeighbourFigure(Figure current)
    {
        float minDistance = float.MaxValue;
        Figure neighbour = null;
        foreach (Figure figure in figures)
            if (figure != current)
            {
                float distance = (current.transform.position - figure.transform.position).magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    neighbour = figure;
                }
            }
        return neighbour;
    }

    public Vector2 GetEmptyPosition()
    {
        Vector2 position = new Vector2();
        for (int i = 0; i < 1000; i++)
        {
            position = new Vector2(Random.Range(-field.width / 2, field.width / 2), Random.Range(-field.height / 2, field.height / 2));
            if (CheckEmptyPosition(position))
                break;
        }
        return position;
    }

    public bool CheckEmptyPosition(Vector2 position)
    {
        return !Physics2D.OverlapCircle(position, BuilderFigures.sizeFigure);
    }

    #region UI

    public void ChangeTimeScale()
    {
        Time.timeScale = timeScaleSlider.value;
    }

    #endregion

}
