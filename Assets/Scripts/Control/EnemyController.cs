using UnityEngine;

namespace RPG.Control{
    public class EnemyController : MonoBehaviour{
        [SerializeField] int health;
        // There is no actually a combat system. This is just for dummy zombies
        public void TakaDamage(int damage){
            health = Mathf.Max(health-damage, 0);
            if (health == 0){
                Destroy(gameObject);
            }
        }

    }
}
