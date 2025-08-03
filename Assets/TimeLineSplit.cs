using UnityEngine;

public class TimeLineSplit : MonoBehaviour
{
    public SpriteRenderer top;
    public int height, width;

    // Start is called before the first frame update
    void Start()
    {
        top.transform.localPosition =
            new Vector3(0.4f, height + 0.5f, 2);
        top.size = new Vector2(2, height + 1);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
