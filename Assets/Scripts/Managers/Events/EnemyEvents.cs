using System;
using Assets.Scripts.Enemy;

namespace Assets.Scripts.Managers
{
    public partial class EventManager
    {
        public static EventInvoker<EnemySpawnedEventArgs> EnemySpawned
                = new EventInvoker<EnemySpawnedEventArgs>();
    }

    public class EnemySpawnedEventArgs : EventArgs
    {
        public readonly Enemy.Enemy SpawnedEnemy;

        public EnemySpawnedEventArgs(Enemy.Enemy enemy)
        {
            SpawnedEnemy = enemy;
        }
    }
}