using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Session : MonoBehaviour
{
    private Game _game;
    
    public enum GameState { museum, room1, room2, room3 }
    public GameState currentState;
    public GameState sceneStartingState;

    public void Init(Game boot) {
        _game = boot;
        currentState = sceneStartingState;
        if (currentState == GameState.room1) {
            TeleportToRoom(0);
        }
    }

    public void SessionEnd() { 
        Debug.Log("SessionEnd");
    }

    public void OnNextButtonPress() {
        Debug.Log("OnNextButtonPress");
        if  (currentState == GameState.museum) {
            SceneManager.LoadScene("Heist");
            return;
        }        
        if  (currentState == GameState.room1) {
            currentState = GameState.room2;
            TeleportToRoom(1);
            return;
        }        
        if  (currentState == GameState.room2) {
            currentState = GameState.room3;
            TeleportToRoom(2);
            return;
        }        
        if  (currentState == GameState.room3) {
            SceneManager.LoadScene("Museum");
            return;
        }        
    }

    private void TeleportToRoom(int roomIndex) {
        _game.map.rooms[roomIndex].Enter();
    }
}
