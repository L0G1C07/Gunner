using UnityEngine;

public class BossHealth : EnemyHealth
{
    public GameObject flagPrefab; // Flag object to spawn on boss defeat
    public Transform flagSpawnPoint; // Position where the flag appears

    public override void Die()
    {
        base.Die(); // Call parent Die() method (destroys boss)

        if (flagPrefab != null && flagSpawnPoint != null)
        {
            Instantiate(flagPrefab, flagSpawnPoint.position, Quaternion.identity);
        }
    }
}
