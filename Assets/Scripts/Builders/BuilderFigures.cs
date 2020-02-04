using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuilderFigures
{
    public static event System.Action<Figure> OnFigureCreate;

    public Figure CurrentFigure { get; set; }

    public abstract FigureBehavior Behavior { get; }

    public abstract void CreateObj();

    public abstract void SetColor();

    /// <summary>
    /// Создаем фигуру по заданным параметрам
    /// </summary>
    public Figure CreateFigure(int angleCount, Vector2 position)
    {
        Figure figure = CurrentFigure;
        CurrentFigure.behavior = Behavior;
        figure.transform.position = position;
        figure.angleCount = angleCount;
        float sizeFigure = Mathf.Clamp(.5f * angleCount, 1, 50);
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < angleCount; i++)
            points.Add(Simple.GetVector2Angle(i * (360f / angleCount)) * sizeFigure / 2);
        LineRenderer line = figure.GetComponent<LineRenderer>();
        line.positionCount = angleCount;
        line.SetPositions(points.ToVector3List().ToArray());
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
}

public class BuilderIdle : BuilderFigures
{
    public override FigureBehavior Behavior { get => FigureBehavior.Idle; }

    public override void CreateObj()
    {
        GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Figure"));
        CurrentFigure = obj.AddComponent<FigureIdle>();
    }

    public override void SetColor()
    {
        LineRenderer line = CurrentFigure.GetComponent<LineRenderer>();
        line.startColor = line.endColor = Color.green;
    }
}

public class BuilderPurposeful : BuilderFigures
{
    public override FigureBehavior Behavior { get => FigureBehavior.Purposeful; }

    public override void CreateObj()
    {
        GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Figure"));
        CurrentFigure = obj.AddComponent<FigurePurposeful>();
    }

    public override void SetColor()
    {
        LineRenderer line = CurrentFigure.GetComponent<LineRenderer>();
        line.startColor = line.endColor = Color.blue;
    }
}

public class BuilderChangeable : BuilderFigures
{
    public override FigureBehavior Behavior { get => FigureBehavior.Changeable; }

    public override void CreateObj()
    {
        GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Figure"));
        CurrentFigure = obj.AddComponent<FigureChangeable>();
    }

    public override void SetColor()
    {
        LineRenderer line = CurrentFigure.GetComponent<LineRenderer>();
        line.startColor = line.endColor = Color.yellow;
    }
}

public class BuilderAggressive : BuilderFigures
{
    public override FigureBehavior Behavior { get => FigureBehavior.Aggressive; }

    public override void CreateObj()
    {
        GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Figure"));
        CurrentFigure = obj.AddComponent<FigureAggressive>();
    }

    public override void SetColor()
    {
        LineRenderer line = CurrentFigure.GetComponent<LineRenderer>();
        line.startColor = line.endColor = Color.red;
    }
}