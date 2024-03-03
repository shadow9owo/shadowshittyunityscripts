using UnityEngine;
using UnityEngine.UI;

public class bullet : MonoBehaviour
{
	private bool shoulddestroy = false;
	public Text count;
	private AudioSource sfx;
	private BoxCollider2D colb;
	private SpriteRenderer sp;

    private void Start()
    {
		sfx = this.GetComponent<AudioSource>();
		sp = this.GetComponent<SpriteRenderer>();
		colb = this.GetComponent<BoxCollider2D>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.tag == "wall")
		{
			colb.enabled = false;
			sp.enabled = false;
			shoulddestroy = true;
		}
		else if (col.collider.tag == "enemy")
		{
			colb.enabled = true;
			sp.enabled = false;
			shoulddestroy = true;
		}
		else if (col.collider.tag == "Player")
        {
			colb.enabled = false;
			sp.enabled = false;
			shoulddestroy = true;
		}
	}
    private void Update()
    {
		if (shoulddestroy = true && !sfx.isPlaying)
        {
			Destroy(this.gameObject);
		}
    }
}
