using UnityEngine;

public class Map : MonoBehaviour
{
    private Game _game; 
    public Room[] rooms;

    public void Init(Game game) {
        _game = game;
    }    
}

