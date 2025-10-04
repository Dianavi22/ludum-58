using NUnit.Framework;
using UnityEngine;

public class Respawnable : MonoBehaviour
{
    [SerializeField] Vector3 _respawnPoint;

	private void Awake()
	{
        Assert.AreNotEqual(_respawnPoint, null);
	}

	public void Respawn()
    {
        gameObject.transform.position = _respawnPoint;
    }
}
