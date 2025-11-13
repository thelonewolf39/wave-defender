using UnityEngine;

public class EnemyDeathNotifier : MonoBehaviour
{
    [HideInInspector]
    public EnemyWaveSpawner spawner;

    // Call this when your enemy dies
    public void OnDestroy()
    {
        if (spawner != null)
            spawner.OnEnemyKilled();
    }
}
