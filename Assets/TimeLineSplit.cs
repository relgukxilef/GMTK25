using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineSplit : MonoBehaviour
{
    public SpriteRenderer top, bottom;
    public int height, width;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        top.transform.localPosition =
            new Vector3(1, height + 0.5f, 2);
        bottom.transform.localPosition =
            new Vector3(2, - 0.5f, 2);
        top.size = new Vector2(width - 1, height - 0.5f);
    }
}
