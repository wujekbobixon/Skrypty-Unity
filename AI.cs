using UnityEngine;
using System.Collections;

public class DobreAI : MonoBehaviour {

	
	public float predkoscObrotu = 6.0f;
 	public bool gladkiObrot = true;
	public float predkoscRuchu = 5.0f; 
	public float rangelook = 30f;
	public float odstepOdGracza = 2f;

	private Transform mojObiekt; 
	private Transform gracz;
	private bool patrzNaGracza = false;
	private Vector3 pozycjaGraczaXYZ; 

	// Use this for initialization
	void Start () {
		mojObiekt = transform; 
		if (GetComponent<Rigidbody> ()) {
			GetComponent<Rigidbody> ().freezeRotation = true;
		}
	}

	// Update is called once per frame
	void Update () {
		gracz = GameObject.FindWithTag("Player").transform; 
		pozycjaGraczaXYZ = new Vector3(gracz.position.x, mojObiekt.position.y, gracz.position.z); // Pobieranie XYZ z gracza
		float dist = Vector3.Distance (mojObiekt.position, gracz.position); // zmienna.distance =(x-y)

		patrzNaGracza = false; 
		if(dist <= rangelook && dist > odstepOdGracza) {
			patrzNaGracza = true;
			mojObiekt.position = Vector3.MoveTowards(mojObiekt.position, pozycjaGraczaXYZ, predkoscRuchu * Time.deltaTime);//zmienna.MoveTowards aktualizuje nowa pozycje

		} else if(dist <= odstepOdGracza) { 
			patrzNaGracza = true;
		}

		patrzNaMnie ();
	}

	void patrzNaMnie(){
		if (gladkiObrot && patrzNaGracza == true){

			//Quaternion.LookRotation - zwraca quaternion na podstawie werktora kierunku/pozycji
			Quaternion rotation = Quaternion.LookRotation(pozycjaGraczaXYZ - mojObiekt.position);
			mojObiekt.rotation = Quaternion.Slerp(mojObiekt.rotation, rotation, Time.deltaTime * predkoscObrotu);
		} else if(!gladkiObrot && patrzNaGracza == true){ 
		
			//Błyskawiczny obrót wroga.
			transform.LookAt(pozycjaGraczaXYZ);
		}

	}
}
