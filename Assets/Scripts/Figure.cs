using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FigureBehavior
{
    Idle,
    RandomDir,
    RandomDirTime,
    Agressive,
}

public class Figure : MonoBehaviour
{
    public static event System.Action<Figure, Figure> OnFigureCollision;

    const float timeChangeDir = 3;
    const float speed = 3;
    const float speedRotate = 30;

    public FigureBehavior behavior;
    public int angleCount;

    Vector3 dir;
    Figure target;
    float currentTimeChangeDir;

    public float Angle
    {
        get { return transform.localEulerAngles.z; }
        set { transform.localEulerAngles = new Vector3(0, 0, value); }
    }

    void Start()
    {
        switch (behavior)
        {
            case FigureBehavior.Idle:
                break;
            case FigureBehavior.RandomDir:
                ChangeDir();
                break;
            case FigureBehavior.RandomDirTime:
                ChangeDir();
                currentTimeChangeDir = timeChangeDir;
                break;
            case FigureBehavior.Agressive:
                SelectTarget();
                break;
        }
    }

    void Update()
    {
        if (behavior == FigureBehavior.RandomDirTime)//Отсчет времени для непостоянных
        {
            currentTimeChangeDir -= Time.deltaTime;
            if (currentTimeChangeDir < 0)
            {
                ChangeDir();
                currentTimeChangeDir = timeChangeDir;
            }
        }
        if (behavior != FigureBehavior.Idle)//Движение если не отдыхает
            transform.position += dir.normalized * speed * Time.deltaTime;
        if (behavior == FigureBehavior.Agressive)
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
            }
        }
    }

    void ChangeDir()
    {
        Vector3 newDir = dir;
        while (dir == newDir)
            newDir = Simple.GetVector2Angle((Random.Range(0, angleCount) + .5f) * (360f / angleCount) + Angle);
        dir = newDir;
    }

    void SelectTarget()
    {
        target = GameManager.Instance.GetNeighbourFigure(this);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Figure neighbour = collision.gameObject.GetComponent<Figure>();
        if (neighbour)//Столкновение с другим жителем
        {
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);
            OnFigureCollision?.Invoke(this, neighbour);
        }
        else//Столкновение с препядствием
        {
            float angleNormal = Simple.GetAngle(collision.contacts[0].normal);
            float currentAngle = 0;
            float delta = 0;
            int sch = 0;
            do//Выбираем другой угол если выбран поворот при котором можем уйти в стену
            {
                ChangeDir();
                currentAngle = Simple.GetAngle(dir);
                delta = angleNormal - currentAngle;
                sch++;
                if (sch > 100)
                {
                    Debug.LogError("Неправильное направление: " + name);
                    break;
                }
            } while (delta > 90 || delta < -90);
            if (behavior == FigureBehavior.RandomDirTime)
                currentTimeChangeDir = timeChangeDir;
        }
    }
}
