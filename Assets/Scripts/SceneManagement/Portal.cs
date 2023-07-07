using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace RPG.SceneManagemetn{
    public class Portal : MonoBehaviour{
        enum PortalIdentifier{
            A,B,C // If i want to use multiple portals
        }
        [SerializeField] PortalIdentifier portalIdentifier;
        [SerializeField] int sceneToLoad;
        [SerializeField] Transform spawnPoint;
        GameObject player; // I need this to carry player to spawn point

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")){
                player = other.gameObject;
                StartCoroutine(Transfer());
            }
        }

        private IEnumerator Transfer(){
            DontDestroyOnLoad(gameObject); // Wait for end of coroutine
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
                if (portal == this || portal.portalIdentifier != portalIdentifier) continue;
                // if (portal.portalIdentifier != portalIdentifier) continue;
                return portal;
            }
            return null;
        }
    }
}
