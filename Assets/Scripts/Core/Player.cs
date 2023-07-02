using UnityEngine;

namespace RPG.Core
{
    [CreateAssetMenu(fileName = "Player", menuName = "RPG-Sample/Player", order = 0)]
    public class Player : ScriptableObject {
        public float speed;

        public float healh;
       [SerializeField] Inventory inventory;

    }

}
