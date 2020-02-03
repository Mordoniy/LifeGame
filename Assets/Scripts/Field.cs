using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public BoxCollider2D top, right, down, left;
    public Camera cam;
    public float width, height;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetBorder(float width, float height)
    {
        this.width = width;
        this.height = height;
        top.transform.position = new Vector3(0, height / 2 + .5f);
        down.transform.position = new Vector3(0, -height / 2 - .5f);
        right.transform.position = new Vector3(width / 2 + .5f, 0);
        left.transform.position = new Vector3(-width / 2 - .5f, 0);
        top.transform.localScale = down.transform.localScale = new Vector3(width, 1, 1);
        right.transform.localScale = left.transform.localScale = new Vector3(1, height, 1);
        cam.orthographicSize = Mathf.Max(height / 2, width / 2 * Screen.height / Screen.width);
        gameObject.SetActive(true);
    }
}
