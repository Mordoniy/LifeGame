using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public BoxCollider2D top, right, down, left;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetBorder(float width, float height)
    {
        top.transform.position = new Vector3(0, height / 2 + .5f);
        down.transform.position = new Vector3(0, -height / 2 - .5f);
        right.transform.position = new Vector3(width / 2 + .5f, 0);
        left.transform.position = new Vector3(-width / 2 - .5f, 0);
        top.size = down.size = new Vector2(width, 1);
        right.size = left.size = new Vector2(height, 1);
    }
}
