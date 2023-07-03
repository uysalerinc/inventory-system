
using UnityEngine;

namespace RPG.Core{
    public class ItemSpawner : MonoBehaviour{
        [SerializeField] Item itemPrefab;
        private void Awake() {
            Item item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
