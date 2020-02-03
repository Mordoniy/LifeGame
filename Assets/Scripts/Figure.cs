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
    public static event System.Action<Figure> OnFigureDeath;

    public FigureBehavior behavior;
    public int angleCount;

    public float Angle
    {
        get { return transform.localEulerAngles.z; }
        private set { transform.localEulerAngles = new Vector3(0, 0, value); }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
