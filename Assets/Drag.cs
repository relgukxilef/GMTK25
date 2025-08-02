using UnityEngine;

public class Drag : MonoBehaviour
{
    public Vector2 start;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            start = point;

        if (!Input.GetMouseButton(0))
            return;

        transform.position += (Vector3)(start - point);
    }
}
