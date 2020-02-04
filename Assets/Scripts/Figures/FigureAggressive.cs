using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureAggressive : Figure
{

    protected override void Start()
    {
        SelectTarget();
    }

    protected override void Update()
    {
        if (!target)//Выбор цели, если текущую съели
            SelectTarget();
        if (target)
        {
            float targetAngle = Simple.GetAngle(target.transform.position - transform.position);//Определение необхдимого угла
            for (int i = 0; i < angleCount; i++)//и поворот до него
            {
                float currentAngle = Angle + (i + .5f) * (360f / angleCount);
                if (currentAngle > 360)
                    currentAngle -= 360;
                else if (currentAngle < 0)
                    currentAngle += 360;

                float delat = Mathf.Abs(targetAngle - currentAngle);
                if (delat < 360f / angleCount / 2 || Mathf.Abs(360 - delat) < 360f / angleCount / 2)
                {
                    if (Mathf.Abs(targetAngle - currentAngle) > speedRotate * Time.deltaTime)
                        if (currentAngle > targetAngle)
                            Angle -= speedRotate * Time.deltaTime;
                        else Angle += speedRotate * Time.deltaTime;
                    else if (Mathf.Abs(targetAngle - currentAngle) != 0)
                        Angle += targetAngle - currentAngle;
                    dir = Simple.GetVector2Angle((i + .5f) * (360f / angleCount) + Angle);
                }
            }
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }

    void SelectTarget()
    {
        target = GameManager.Instance.GetNeighbourFigure(this);
    }
}
