using UnityEngine;
using UnityEngine.EventSystems;

public class CaptureButton : MonoBehaviour, IPointerClickHandler
{
    public Game game;
    public SpriteRenderer spriteRenderer;
    public bool active = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!active)
            return;

        var agent = game.selection;
        game.BeginMove(agent.board);
        foreach (var enemy in agent.board.rooms[agent.Room].enemies)
        {
            enemy.captured = true;
            game.transform.localPosition -= new Vector3(game.timeOffset, 0, 0);
            game.captureCount++;
            // TODO: show victory screen
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        active = game.capturableEnemies.Count > 0 && game.selection;
        spriteRenderer.enabled = active;
    }
}
