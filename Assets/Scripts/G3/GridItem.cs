using System;
using UnityEngine;

namespace Assets.Scripts.G3
{
    [Serializable]
    public class GridItem
    {
        [Serializable]
        public enum ItemType
        { 
            None = 0,
            Controller = 1,
            Blocker = 2,
            Checkpoint = 3,
            Finish = 4,
            Target = 5,
            HalfBlocker = 6,
        }

        [SerializeField]
        public Vector3 position;

        [SerializeField]
        public GameObject prefab;

        [SerializeField]
        public ItemType type;

        private GameObject activeObject;

        public GridItem()
        {
            position = Vector3.zero;
            prefab = null;
            activeObject = null;
            type = ItemType.None;
        }

        public GameObject ActiveObject { get { return activeObject; } set { activeObject = value; }}
    }
}
