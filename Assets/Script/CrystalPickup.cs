using Fusion;
using FusionIntroShared;
using UnityEngine;

public class CrystalPickup : NetworkBehaviour
{
    private bool collected;

    public void TryCollect(Player player)
    {
        if (collected || player == null)
        {
            return;
        }

        if (!Object.HasStateAuthority)
        {
            return;
        }

        collected = true;
        player.AddPoint();

        Runner.Despawn(Object);
    }
}
