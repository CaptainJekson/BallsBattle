using System;
using System.Collections.Generic;
using UnityEngine;

namespace Serializable
{
    [Serializable]
    public class Save
    {
        public List<BallSaveData> BallSaveDatas;
        
        public Save()
        {
            BallSaveDatas = new List<BallSaveData>();
        }
    }

    [Serializable]
    public class BallSaveData
    {
        public Vector3 Position;
        public float Radius;
        public float UnitDestroyRadius;
        public float Speed;
        public Vector3 Direction;
        public bool IsEnemy;
        
        public BallSaveData(Vector3 position, float radius, float unitDestroyRadius, float speed, 
            Vector3 direction, bool isEnemy)
        {
            Position = position;
            Radius = radius;
            UnitDestroyRadius = unitDestroyRadius;
            Speed = speed;
            Direction = direction;
            IsEnemy = isEnemy;
        }
    }
}