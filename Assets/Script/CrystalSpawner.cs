using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CrystalSpawner : NetworkBehaviour
{
    public NetworkObject crystalPrefab;
    public Transform[] spawnPoints;

    public int maxCrystals = 1;
    public float respawnDelay = 1.5f;

    private List<NetworkObject> crystals = new List<NetworkObject>();

    public override void Spawned()
    {
        Debug.Log("CrystalSpawner iniciou!");

        Debug.Log("State Authority: " + Object.HasStateAuthority);

        if (!Object.HasStateAuthority)
        {
            return;
        }

        for (int i = 0; i < maxCrystals; i++)
        {
            SpawnCrystal();
        }
    }

    public void SpawnCrystal()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }

        if (crystalPrefab == null)
        {
            Debug.LogError("Crystal Prefab não foi configurado!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Nenhum Spawn Point foi configurado!");
            return;
        }

        crystals.RemoveAll(crystal => crystal == null);

        if (crystals.Count >= maxCrystals)
        {
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        NetworkObject crystal = Runner.Spawn(crystalPrefab, spawnPoint.position, spawnPoint.rotation);

        if (crystal != null)
        {
            crystals.Add(crystal);
            Debug.Log("Cristal criado em: " + spawnPoint.position);
        }
        else
        {
            Debug.LogError("Runner.Spawn não criou o cristal!");
        }
    }

    public void CrystalCollected(NetworkObject crystal)
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }

        if (!crystals.Remove(crystal))
        {
            return;
        }

        StartCoroutine(RespawnCrystal());
    }

    private IEnumerator RespawnCrystal()
    {
        yield return new WaitForSeconds(respawnDelay);

        SpawnCrystal();
    }
}
