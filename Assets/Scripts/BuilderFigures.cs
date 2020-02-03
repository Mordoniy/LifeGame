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
    public const float sizeFigure = 1;

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
        figure.Angle = Random.Range(0, 360);
        figure.name = "Figure №" + GameManager.Instance.figures.Count;

        OnFigureCreate?.Invoke(figure);
        return figure;
    }

    public static Figure CreateFigure(int angleCount, Vector2 position)
    {
        return CreateFigure(angleCount, (FigureBehavior)Random.Range(0, 4), position);
    }

    public static Figure CreateFigure(int angleCount)
    {
        return CreateFigure(angleCount, (FigureBehavior)Random.Range(0, 4), GameManager.Instance.GetEmptyPosition());
    }

    public static Figure CreateFigure()
    {
        return CreateFigure(Random.Range(2, 6), (FigureBehavior)Random.Range(0, 4), GameManager.Instance.GetEmptyPosition());
    }
}
