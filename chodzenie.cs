using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chodzenie : MonoBehaviour
{
    public CharacterController characterControler;
        public float speed = 9.0f;
        public float heightjump = 7.0f;
        public float nowheightjump = 0.0f;
        public float running = 7.0f;
	public float czuloscMyszki = 3.0f;
	public float myszGoraDol = 0.0f;
	//Zakres patrzenia w górę i dół.
	public float zakresMyszyGoraDol = 90.0f;

	// Start is called before the first frame update
	void Start()
    {
        characterControler = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        klawiatura();
        myszka();
    }
	private void klawiatura()
	{
		// jeżeli wartość dodatnia to poruszamy się do przodu,
		// jeżeli wartość ujemna to poruszamy się do tyłu.
		float rochPrzodTyl = Input.GetAxis("Vertical") * speed;
		//Pobranie prędkości poruszania się lewo/prawo.
		// jeżeli wartość dodatnia to poruszamy się w prawo,
		// jeżeli wartość ujemna to poruszamy się w lewo.
		float rochLewoPrawo = Input.GetAxis("Horizontal") * speed;
		//Debug.Log (rochLewoPrawo);

		//Skakanie
		if (characterControler.isGrounded && Input.GetButton("Jump"))
		{
			nowheightjump = heightjump;
		}
		else if (!characterControler.isGrounded) { 
			nowheightjump += Physics.gravity.y * Time.deltaTime;
		}

		//Debug.Log(Physics.gravity.y);

		//Bieganie
		if (Input.GetKeyDown("left shift"))
		{
			speed += running;
		}
		else if (Input.GetKeyUp("left shift"))
		{
			speed -= running;
		}

		Vector3 ruch = new Vector3(rochLewoPrawo, nowheightjump, rochPrzodTyl);
		ruch = transform.rotation * ruch;

		characterControler.Move(ruch * Time.deltaTime);
	}


	private void myszka()
	{
		// jeżeli wartość dodatnia to poruszamy w prawo,
		// jeżeli wartość ujemna to poruszamy w lewo.
		float myszLewoPrawo = Input.GetAxis("Mouse X") * czuloscMyszki;
		transform.Rotate(0, myszLewoPrawo, 0);

		// jeżeli wartość dodatnia to poruszamy w górę,
		// jeżeli wartość ujemna to poruszamy w dół.
		myszGoraDol -= Input.GetAxis("Mouse Y") * czuloscMyszki;

		//Funkcja nie pozwala aby wartość przekroczyła dane zakresy.
		myszGoraDol = Mathf.Clamp(myszGoraDol, -zakresMyszyGoraDol, zakresMyszyGoraDol);
	//Obrucenie kamery a nie characyterem
		Camera.main.transform.localRotation = Quaternion.Euler(myszGoraDol, 0, 0);
	}
}
