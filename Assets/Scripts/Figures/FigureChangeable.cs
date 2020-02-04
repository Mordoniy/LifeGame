using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureChangeable : Figure
{

    protected override void Start()
    {
        ChangeDir();
        currentTimeChangeDir = timeChangeDir;
    }

    protected override void Update()
    {
        transform.position += dir.normalized * speed * Time.deltaTime;
        currentTimeChangeDir -= Time.deltaTime;
        if (currentTimeChangeDir < 0)
        {
            ChangeDir();
            currentTimeChangeDir = timeChangeDir;
        }
    }
}
