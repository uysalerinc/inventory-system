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
        [SerializeField] GameObject weaponPlace;
        InventoryController inventoryController;
        #endregion
        #region  Cache Referances
        Vector2 moveDirection;
        #endregion
        private void Awake() {
            // Declare components
            playerData.weaponPlace = weaponPlace;
            playerData.equippedWeapon = null;
            inventoryController = GetComponent<InventoryController>();
            rb = GetComponent<Rigidbody2D>();
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            inventoryController.playerData = playerData;
            // For scene load
            DontDestroyOnLoad(gameObject);
        }

        private void Update(){
            Movement();
            Attack();
        }
        // Get axis from input
        private void Movement(){
            moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            animator.SetBool("isRunning", moveDirection.magnitude != 0);
            if (moveDirection[0] != 0){
                transform.localScale = new Vector3(moveDirection[0], 1, 1);
            }
        }

        private void FixedUpdate() {
            mover.MoveTo(moveDirection, playerData.speed, rb); // Move to given direction
        }
        // Just swing weapon on hand
        private void Attack(){
            if (Input.GetKeyDown(KeyCode.Space) && playerData.equippedWeapon != null){
                playerData.equippedWeapon.GetComponent<Animator>().SetTrigger("swing");
            }
        }
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Enemy")){
                other.GetComponent<EnemyController>().TakaDamage(20);
            }
        }
    }
}
