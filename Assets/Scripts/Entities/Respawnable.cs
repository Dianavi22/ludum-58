using UnityEngine;

public class Respawnable : MonoBehaviour
{
    [SerializeField] Vector3 _respawnPoint;

	public void Respawn()
    {
        gameObject.transform.position = _respawnPoint;
    }
}
