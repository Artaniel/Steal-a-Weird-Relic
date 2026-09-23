using UnityEngine;

public class Room : MonoBehaviour
{
    private Game _game;
    private Map _map;    
    public Transform startPoint;

    public void Init(Game game, Map map) {
        _game = game;
        _map = map;
    }
    
    public void Enter() {
        _game.player.transform.position = startPoint.transform.position;
        _game.player.transform.rotation = startPoint.transform.rotation;    
    }
}

