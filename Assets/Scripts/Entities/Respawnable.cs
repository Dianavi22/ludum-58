using UnityEngine;

public class Respawnable : MonoBehaviour
{
    [SerializeField] Transform _respawnPoint;

	public void Respawn()
    {
        gameObject.transform.position = _respawnPoint.position;
    }
}
