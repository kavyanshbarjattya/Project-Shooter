using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected Transform player;

    public abstract EnemyType GetEnemyType();
    public abstract void Initialize();

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }
}