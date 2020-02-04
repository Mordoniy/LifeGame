using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider timeScaleSlider;
    public List<Figure> figures;
    public Field field;
    Dictionary<FigureBehavior, BuilderFigures> builders;
    Builder builder;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        BuilderFigures.OnFigureCreate += OnCreateFigure;
        Figure.OnFigureCollision += OnFigureCollision;
        builder = new Builder();
        builders = new Dictionary<FigureBehavior, BuilderFigures>
        {
            { FigureBehavior.Idle, new BuilderIdle() },
            { FigureBehavior.Purposeful, new BuilderPurposeful() },
            { FigureBehavior.Changeable, new BuilderChangeable() },
            { FigureBehavior.Aggressive, new BuilderAggressive() }
        };
    }

    //События при создании и столкновнеии фигур
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
            FigureBehavior behavior = a.behavior;
            if (b.angleCount > a.angleCount)
                behavior = b.behavior;
            builder.CreateFigure(builders[behavior], a.angleCount + b.angleCount, (a.transform.position + b.transform.position) / 2);
        }
    }

    public void Play(int count)
    {
        for (int i = 0; i < count; i++)
            builder.CreateFigure(builders[BuilderSettings.GetRandomBehavior()]);
    }

    /// <summary>
    /// Получаем ближайшую фигуру
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    public Figure GetNeighbourFigure(Figure current)
    {
        float minDistance = float.MaxValue;
        Figure neighbour = null;
        foreach (Figure figure in figures)
            if (figure && figure != current)
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

    /// <summary>
    /// Поиск пустого места, если такого нет - выдает случайное
    /// </summary>
    /// <returns></returns>
    public Vector2 GetEmptyPosition(float size)
    {
        Vector2 position = new Vector2();
        for (int i = 0; i < 1000; i++)
        {
            position = new Vector2(Random.Range(-field.width / 2, field.width / 2), Random.Range(-field.height / 2, field.height / 2));
            if (CheckEmptyPosition(position, size))
                break;
        }
        return position;
    }

    /// <summary>
    /// Проверяем пустует ли место
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool CheckEmptyPosition(Vector2 position, float size)
    {
        return !Physics2D.OverlapCircle(position, size);
    }

    #region UI

    public void ChangeTimeScale()
    {
        Time.timeScale = timeScaleSlider.value;
    }

    #endregion

}

public class Builder
{
    public Figure CreateFigure(BuilderFigures builder, int angleCount, Vector2 position)
    {
        builder.CreateObj();
        builder.CreateFigure(angleCount, position);
        builder.SetColor();
        return builder.CurrentFigure;
    }

    public Figure CreateFigure(BuilderFigures builder)
    {
        int angleCount = Random.Range(2, 6);
        return CreateFigure(builder, angleCount, GameManager.Instance.GetEmptyPosition(Mathf.Clamp(.5f * angleCount, 1, 20)));
    }
}
