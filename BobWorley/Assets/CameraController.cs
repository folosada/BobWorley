using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform player;
    public Vector3 offset;

    const float TAMANHO_MAXIMO_HORIZONTAL = 171.5f;
    const float TAMANHO_MAXIMO_VERTICAL = 7.70f;
    const float TAMANHO_MINIMO_HORIZONTAL = 1.45f;
    const float TAMANHO_MINIMO_VERTICAL = -21.45f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float x = player.position.x + offset.x;
        x = x > TAMANHO_MAXIMO_HORIZONTAL ? TAMANHO_MAXIMO_HORIZONTAL : x < TAMANHO_MINIMO_HORIZONTAL ? TAMANHO_MINIMO_HORIZONTAL : x;
        float y = player.position.y + offset.y;
        y = y > TAMANHO_MAXIMO_VERTICAL ? TAMANHO_MAXIMO_VERTICAL : y < TAMANHO_MINIMO_VERTICAL ? TAMANHO_MINIMO_VERTICAL : y;
        transform.position = new Vector3(x, y, offset.z); // Camera follows the player with specified offset position
    }
}
