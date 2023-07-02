using UnityEngine;

namespace RPG.Movement{
    public class Mover : MonoBehaviour{
        public void MoveTo(Vector2 direction, float speed, Rigidbody2D rb){
            rb.velocity = direction.normalized * speed;
        }

    }
}
