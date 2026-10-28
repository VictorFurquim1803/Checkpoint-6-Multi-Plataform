using UnityEngine;
using Fusion;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Vector3 spawnPosition = new Vector3(player.RawEncoded * 3f, 1f, 0f);

            Runner.Spawn(PlayerPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
