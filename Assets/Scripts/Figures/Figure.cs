using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FigureBehavior
{
    Idle,
    Purposeful,
    Changeable,
    Aggressive,
}

public abstract class Figure : MonoBehaviour
{
    public static event System.Action<Figure, Figure> OnFigureCollision;

    protected const float timeChangeDir = 3;
    protected const float speed = 3;
    protected const float speedRotate = 30;

    public FigureBehavior behavior;
    public int angleCount;

    protected Vector3 dir;
    protected Figure target;
    protected float currentTimeChangeDir;

    public float Angle
    {
        get { return transform.localEulerAngles.z; }
        set { transform.localEulerAngles = new Vector3(0, 0, value); }
    }

    protected abstract void Start();

    protected abstract void Update();

    protected void ChangeDir()
    {
        Vector3 newDir = dir;
        while (dir == newDir)
            newDir = Simple.GetVector2Angle((Random.Range(0, angleCount) + .5f) * (360f / angleCount) + Angle);
        dir = newDir;
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
            if (GetType() == typeof(FigureChangeable))
                currentTimeChangeDir = timeChangeDir;
        }
    }
}
