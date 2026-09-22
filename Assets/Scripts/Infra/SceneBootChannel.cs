using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Channels/SceneBootChannel", fileName = "SceneBootChannel")]
public class SceneBootChannel : ScriptableObject
{
    public Boot boot;

    public void GameCreatedSignal(Game game) {
        boot?.OnBootCreated(game);
    }
} 