using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoJogador : MonoBehaviour {
    private Rigidbody rb;
    public float velocidade;
    private bool pulando = false;
    public float forcaDoPulo;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate() {
        float mh = Input.GetAxis("Horizontal");
        float mv = Input.GetAxis("Vertical");
        float pulo = 0.0f;
        if (Input.GetKey(KeyCode.Space) && pulando == false) {
            pulo = forcaDoPulo;
            pulando = true;
        }
        Vector3 forca = new Vector3(mh, pulo, mv);
        rb.AddForce(forca * velocidade);        
    }
    void OnCollisionEnter(Collision col) {
        pulando = false;
    }
}
