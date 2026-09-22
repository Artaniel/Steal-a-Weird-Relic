using UnityEngine;
using System.Collections.Generic;

public class PlayersFactory : MonoBehaviour
{
    private Game _game;
    
    public Player myPlayer;

    private bool needRefreshLeaderboard = false;

    public void Init(Game game) {
        _game = game;
    }
   
}
