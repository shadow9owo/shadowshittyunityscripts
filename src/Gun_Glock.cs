using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun_Glock : MonoBehaviour
{
	public GameObject bulletPrefab;

	public Transform bulletSpawnPoint;

	public GameObject cursor;

	public float shootDelay = 0.5f;

	public float bulletSpeed = 10f;

	public float bulletLifetime = 2f;

	public Animator anim;

	public AudioSource outofammo;

	public camerashake camerashakeobj;

	private void Update()
	{
		if (!globalvars.paused && Input.GetKeyDown((KeyCode)globalvars.keybinds[5]) && globalvars.canshoot && !globalvars.shopzone)
		{
			if (SceneManager.GetActiveScene().buildIndex == 5 && globalvars.totalammo > 0)
            {
				globalvars.totalammo = globalvars.totalammo -1;
                Shoot();
		    }
			else if (SceneManager.GetActiveScene().buildIndex == 5 && globalvars.totalammo <= 0)
            {
				outofammo.Play();
            }
			else if (SceneManager.GetActiveScene().buildIndex != 5)
            {
				Shoot();
			}
			globalvars.canshoot = false;
			StartCoroutine(ResetShoot());
		}
	}

	private IEnumerator ResetShoot()
	{
		yield return new WaitForSeconds(shootDelay);
		globalvars.canshoot = true;
	}

	private void Shoot()
	{
		StartCoroutine(camerashakeobj.Shake(0.2f, 0.05f));
		anim.SetInteger("shouldshot", 1);
		Vector2 vector = new Vector2(cursor.transform.position.x, cursor.transform.position.y);
		GameObject gameObject = Object.Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
		gameObject.SetActive(true);
		Vector2 normalized = (vector - (Vector2)bulletSpawnPoint.position).normalized;
		gameObject.transform.right = normalized;
		Rigidbody2D component = gameObject.GetComponent<Rigidbody2D>();
		component.velocity = normalized * bulletSpeed;
		Object.Destroy(gameObject, bulletLifetime);
		anim.SetInteger("shouldshot", 0);
	}
}
