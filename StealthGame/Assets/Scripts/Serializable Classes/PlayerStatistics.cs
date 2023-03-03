using System;
using UnityEngine;

namespace SerializableClass
{
    [Serializable]
    public class PlayerStatistics
    {
        public float timeTaken;
        public int guardsKilled;
        public int teleportsUsed;
        public int distractsUsed;
        public string dateTime;
    }
}


