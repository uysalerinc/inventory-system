using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using RPG.Core;

namespace RPG.SceneManagemetn{
    public class Portal : MonoBehaviour{
        enum PortalIdentifier{
            A,B,C
        }
        [SerializeField] PortalIdentifier portalIdentifier;
        [SerializeField] int sceneToLoad;
        [SerializeField] Transform spawnPoint;
        GameObject inventoryUI;
        GameObject player;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")){
                player = other.gameObject;
                inventoryUI = GameObject.FindWithTag("UI");
                StartCoroutine(Transfer());
            }
        }

        private IEnumerator Transfer(){
            CarryToNextScene();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Portal otherPortal = GetOtherPortal();
            TransferPlayer(otherPortal);
            Destroy(gameObject);
        }

        private void TransferPlayer(Portal otherPortal){
            player.transform.position = otherPortal.spawnPoint.position;
            Destroy(otherPortal);
            }
            
        private Portal GetOtherPortal(){
            foreach (Portal portal in FindObjectsOfType<Portal>()){
                if (portal == this) continue;
                if (portal.portalIdentifier != portalIdentifier) continue;

                Debug.Log("buldu");
                return portal;
            }

            Debug.Log("PortalıBulamadıMal");
            return null;
        }
        private void CarryToNextScene(){
            DontDestroyOnLoad(gameObject);
        }
    }

}
