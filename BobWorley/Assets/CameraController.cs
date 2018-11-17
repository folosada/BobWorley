using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform player;
    public Vector3 offset;

    public float TamanhoMaximoHorizontal;
    public float TamanhoMaximoVertical;
    public float TamanhoMinimoHorizontal;
    public float TamanhoMinimoVertical;


    //const float TAMANHO_MAXIMO_HORIZONTAL = 171.5f;
    //const float TAMANHO_MAXIMO_VERTICAL = 7.70f;
    //const float TAMANHO_MINIMO_HORIZONTAL = 1.45f;
    //const float TAMANHO_MINIMO_VERTICAL = -21.45f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float x = player.position.x + offset.x;
        x = x > TamanhoMaximoHorizontal ? TamanhoMaximoHorizontal : x < TamanhoMinimoHorizontal ? TamanhoMinimoHorizontal: x;
        float y = player.position.y + offset.y;
        y = y > TamanhoMaximoVertical ? TamanhoMaximoVertical : y < TamanhoMinimoVertical ? TamanhoMinimoVertical : y;
        transform.position = new Vector3(x, y, offset.z); // Camera follows the player with specified offset position
    }
}
