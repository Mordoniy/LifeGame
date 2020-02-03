using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuilderFigures
{
    public static event System.Action<Figure> OnFigureCreate;
    static Color[] colors = new Color[]
    {
        Color.green,
        Color.blue,
        Color.yellow,
        Color.red,
    };

    public static float[] percentBehavior = new float[] { 25, 25, 25, 25 };
    public const float sizeFigure = 1;

    /// <summary>
    /// Создаем фигуру по заданным параметрам
    /// </summary>
    /// <param name="angleCount"></param>
    /// <param name="behavior"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Figure CreateFigure(int angleCount, FigureBehavior behavior, Vector2 position)
    {
        Figure figure = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Figure")).GetComponent<Figure>();
        figure.transform.position = position;
        figure.behavior = behavior;
        figure.angleCount = angleCount;
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < angleCount; i++)
            points.Add(Simple.GetVector2Angle(i * (360f / angleCount)) * sizeFigure / 2);
        LineRenderer line = figure.GetComponent<LineRenderer>();
        line.positionCount = angleCount;
        line.SetPositions(points.ToVector3List().ToArray());
        line.startColor = line.endColor = colors[(int)behavior];
        if (points.Count == 2)
        {
            points.Add(new Vector2(points[1].x, .05f));
            points.Add(new Vector2(points[0].x, .05f));
        }
        figure.GetComponent<PolygonCollider2D>().points = points.ToArray();
        figure.Angle = Random.Range(0, 360f);
        figure.name = "Figure №" + GameManager.Instance.figures.Count;

        OnFigureCreate?.Invoke(figure);
        return figure;
    }

    public static Figure CreateFigure(int angleCount, Vector2 position)
    {
        return CreateFigure(angleCount, GetRandomBehavior(), position);
    }

    public static Figure CreateFigure()
    {
        return CreateFigure(Random.Range(2, 6), GetRandomBehavior(), GameManager.Instance.GetEmptyPosition());
    }

    /// <summary>
    /// Получаем случайное поведение на основании заданных процентов
    /// </summary>
    /// <returns></returns>
    public static FigureBehavior GetRandomBehavior()
    {
        float value = Random.Range(0, 100);
        for (int i = 0; i < percentBehavior.Length; i++)
        {
            value -= percentBehavior[i];
            if (value <= 0)
                return (FigureBehavior)i;
        }
        return (FigureBehavior)percentBehavior.Length - 1;
    }

    /// <summary>
    /// При изменении одного процента меняем остальные, чтобы всегда сохранялось 100%
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    public static void ChangePercent(int index, float value)
    {
        float delta = value - percentBehavior[index];
        percentBehavior[index] = value;
        float sum = 0;
        for (int i = 0; i < percentBehavior.Length; i++)
            if (i != index)
                sum += percentBehavior[i];
        for (int i = 0; i < percentBehavior.Length; i++)
            if (i != index)
                percentBehavior[i] -= delta * (percentBehavior[i] / sum);
    }
}
