using UnityEngine;
using UnityEngine.UI;

public class bullet : MonoBehaviour
{
	public Text count;

	public AudioSource sfx;

	private Vector3 vel;

	private Rigidbody2D rb;

    private void Start()
    {
		GameObject g = Instantiate(sfx.gameObject);
		g.SetActive(true);
		rb = this.gameObject.GetComponent<Rigidbody2D>();
		vel = rb.velocity;
	}

    private void Update()
    {
		rb.velocity = vel;
    }

    private void OnCollisionEnter2D(Collision2D col)
	{
        if (col.collider.tag == "wall" || col.collider.tag == "box")
        {
            Destroy(this.gameObject);
        }
        else if (col.collider.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        else if (col.collider.tag == "enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
