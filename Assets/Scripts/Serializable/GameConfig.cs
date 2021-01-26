using System;

namespace Serializable
{
    [Serializable]
    public class GameConfig
    {
        public float gameAreaWidth;
        public float gameAreaHeight;
        public float numUnitsToSpawn;
        public float unitSpawnDelay;
        public float unitSpawnMinRadius;
        public float unitSpawnMaxRadius;
        public float unitSpawnMinSpeed;
        public float unitSpawnMaxSpeed;
        public float unitDestroyRadius;
    }
}