using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigurePurposeful : Figure
{

    protected override void Start()
    {
        ChangeDir();
    }

    protected override void Update()
    {
        transform.position += dir.normalized * speed * Time.deltaTime;
    }
}
