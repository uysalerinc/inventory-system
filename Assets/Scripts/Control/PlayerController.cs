using UnityEngine;
using RPG.Core;
using RPG.Movement;
using RPG.UI;

namespace RPG.Control{

    public class PlayerController : MonoBehaviour{
        #region Components
        Rigidbody2D rb;
        Mover mover;
        Animator animator;
        [SerializeField] Player playerData;
        [SerializeField] GameObject weaponPlace;
        InventoryController inventoryController;
        #endregion
        #region  Cache Referances
        Vector2 moveDirection;
        #endregion
        private void Awake() {
            playerData.weaponPlace = weaponPlace;
            inventoryController = GetComponent<InventoryController>();
            rb = GetComponent<Rigidbody2D>();
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            inventoryController.playerData = playerData;
        }
        private void Update(){
            Movement();
        }

        private void Movement(){
            moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            animator.SetBool("isRunning", moveDirection.magnitude != 0);
            if (moveDirection[0] != 0){
                transform.localScale = new Vector3(moveDirection[0], 1, 1);
            }
        }

        private void FixedUpdate() {
            mover.MoveTo(moveDirection, playerData.speed, rb);
        }

    }
}
