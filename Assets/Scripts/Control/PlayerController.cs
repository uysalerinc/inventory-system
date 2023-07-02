using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control{

    public class PlayerController : MonoBehaviour{
        #region Components
        Rigidbody2D rb;
        Mover mover;
        Animator animator;
        [SerializeField] Player playerData;
        #endregion
        #region  Cache Referances
        Vector2 moveDirection;
        #endregion
        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }
        private void Update() {
            moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            animator.SetBool("isRunning", moveDirection.magnitude!=0);
            if (moveDirection[0] != 0){
                transform.localScale = new Vector3(moveDirection[0], 1, 1);
            }
        }
        private void FixedUpdate() {
            mover.MoveTo(moveDirection, playerData.speed, rb);
        }

    }
}
