using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float enemyTurnSpeed = 0.5f;

    public Agent selection;
    public float secondsSinceTurn = 0.0f;
    public new GameObject camera;
    public int timeOffset, lineOffset;
    public int timelines = 1;
    public GameObject timeLineSprite;
    public TimeLineSplit timeLineSplit;
    public List<Enemy> capturableEnemies;
    public int captureCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceTurn += Time.deltaTime;
    }

    void Awake()
    {
        // TODO: This does not work for levels that already have a history
        foreach (var enemy in GetComponentsInChildren<Enemy>())
        {
            enemy.random.seed = (uint)Random.Range(0, 128);
        }
    }

    public void BeginMove(Board board)
    {
        secondsSinceTurn = 0;
        // copy source
        Instantiate<Board>(board, transform).frozen = true;
        // move source to next time step, maybe animated
        board.transform.localPosition += new Vector3(timeOffset, 0, 0);
    }
}
