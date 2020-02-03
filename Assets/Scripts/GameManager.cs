using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public List<Figure> figures;
    public Field field;

    public static GameManager Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {

    }
}
