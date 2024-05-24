using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class Gun_Glock : MonoBehaviour
{
	public GameObject bulletPrefab;

	public Transform bulletSpawnPoint;

	public GameObject cursor;

	public float shootDelay = 0.5f;

	private float bulletSpeed = 250f;

	public float bulletLifetime = 2f;

	private bool modified = false;

	public Animator anim;

	public AudioSource outofammo,reloadsfx;

	public float reloadtime = 1.5f;

	public camerashake camerashakeobj;

	private void Update()
	{
		if (XInputDotNetPure.GamePad.GetState(PlayerIndex.One).IsConnected)
        {
			if (!globalvars.paused && XInputDotNetPure.GamePad.GetState(PlayerIndex.One).Triggers.Right > 0.3f && globalvars.canshoot && !globalvars.shopzone && !globalvars.endless_mode_isinshop && !globalvars.reloading)
			{
				if (globalvars.glockbulletsleft > 0)
				{
					Shoot();
				}
				else if (globalvars.totalammo <= 0)
				{
					outofammo.Play();
				}

				globalvars.canshoot = false;
				StartCoroutine(ResetShoot());
			}
			if (!globalvars.paused && !globalvars.dead)
			{
				if (XInputDotNetPure.GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed && !globalvars.reloading && globalvars.glockbulletsleft < 15)
				{
					StartCoroutine(Reload());
				}
				if (globalvars.glockbulletsleft <= 0 && globalvars.totalammo > 0)
				{
					StartCoroutine(Reload());
				}
			}
		}else
        {
			if (!globalvars.paused && Input.GetKeyDown((KeyCode)globalvars.keybinds[5]) && globalvars.canshoot && !globalvars.shopzone && !globalvars.endless_mode_isinshop && !globalvars.reloading)
			{
				if (globalvars.glockbulletsleft > 0)
				{
					Shoot();
				}
				else if (globalvars.totalammo <= 0)
				{
					outofammo.Play();
				}

				globalvars.canshoot = false;
				StartCoroutine(ResetShoot());
			}
			if (!globalvars.paused && !globalvars.dead)
			{
				if (Input.GetKeyDown((KeyCode)globalvars.keybinds[8]) && !globalvars.reloading && globalvars.glockbulletsleft < 15)
				{
					StartCoroutine(Reload());
				}
				if (globalvars.glockbulletsleft <= 0 && globalvars.totalammo > 0)
				{
					StartCoroutine(Reload());
				}
			}
		}
	}

	private IEnumerator Reload() // 14 ; 14 = 15 ; 13
	{
		if (modified || globalvars.totalammo < 1)
        {
			yield break;
        }
		modified = true;
		globalvars.reloading = true;
		reloadsfx.Play();
		yield return new WaitForSeconds(reloadtime);
		if (globalvars.totalammo - 15 < 0)
		{
			if (globalvars.totalammo - 15 < -14)
            {
				globalvars.totalammo = 0;
			}else
            {
				globalvars.totalammo = globalvars.totalammo + globalvars.glockbulletsleft;

				if (globalvars.glockbulletsleft > 0)
				{
					if (globalvars.totalammo - 15 < 0)
                    {
						globalvars.glockbulletsleft = globalvars.totalammo;
						globalvars.totalammo = 0;
                    }else
                    {
						globalvars.totalammo = globalvars.totalammo - 15;
						globalvars.glockbulletsleft = 15;
					}
				}
			}
		}
		else
		{
			globalvars.totalammo = globalvars.totalammo + globalvars.glockbulletsleft;
			
			if (globalvars.glockbulletsleft > 0)
			{
				globalvars.totalammo = globalvars.totalammo - 15;
			}
			globalvars.glockbulletsleft = 15;
		}
		globalvars.canshoot = true;
		globalvars.reloading = false;
		modified = false;
	}

	private IEnumerator ResetShoot()
	{
		yield return new WaitForSeconds(shootDelay);
		globalvars.canshoot = true;
	}

	private void Shoot()
	{
		if (SceneManager.GetActiveScene().buildIndex == 8)
		{
			globalvars.tutorialgunshoot = true;
		}
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

		globalvars.glockbulletsleft = globalvars.glockbulletsleft - 1;
		return;
	}
}
