using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControleDoJogador : MonoBehaviour {
    private Rigidbody rb;
    public float velocidade;
    private bool pulando = false;
    public float forcaDoPulo;
    private int pontuacao;
    public TextMeshProUGUI txtPontuacao;
    private int posicao = 2;

    private AudioSource somMoeda;
    private AudioSource somPulo;
    private AudioSource somPonto;

    void Start() {
        rb = GetComponent<Rigidbody>();
        pontuacao = 0;

        somMoeda = GetComponents<AudioSource>()[1];
        somPulo = GetComponents<AudioSource>()[2];
        somPonto = GetComponents<AudioSource>()[3];
    }
    void Update() {
        //float mh = Input.GetAxis("Horizontal");
        //float mv = Input.GetAxis("Vertical");

        float pulo = 0.0f;
        if (Input.GetKey(KeyCode.Space) && pulando == false) {
            pulo = forcaDoPulo;
            pulando = true;
            somPulo.Play();
            Vector3 forca = new Vector3(0, pulo,0);
            rb.AddForce(forca * velocidade);        
        }
        //Vector3 forca = new Vector3(mh, pulo, mv);
        //rb.AddForce(forca * velocidade);  

        if (Input.GetKeyDown(KeyCode.LeftArrow) && posicao >1) {
            posicao--;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && posicao < 3) {
            posicao++;
        }

        //define a posição X do jogador
        float x;  
        if (posicao==1) {
            x = -3.79F;
        }  else if (posicao==2) {
            x = -0.3F;
        } else {
            x =  2.72F;
        }
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y,5.5F);               
    }
    void OnCollisionEnter(Collision col) {
        pulando = false;
    }
    void OnTriggerEnter(Collider other) {
        //esconde o outro objeto caso for uma moeda
        if (other.gameObject.CompareTag("Moeda")) {
            other.gameObject.SetActive(false);
            pontuacao++;
            txtPontuacao.text = pontuacao + " pontos";
            somMoeda.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else if (other.gameObject.CompareTag("Cogumelo")) {
            other.gameObject.SetActive(false);
            //dobra o tamanho do jogador
            gameObject.transform.localScale = new Vector3(2,2,2);
            somPonto.Play();
        }
    }

    public void acabou() {
        Application.Quit();
    }
}
