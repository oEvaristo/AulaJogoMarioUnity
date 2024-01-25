using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private CharacterController controle;
    private Animator animacao;

    public float velocidadeMovimento = 10f;
    public float forcaGravidade = 9.8f;
    public float forcaPulo = 2f;
    public float velocidadeRotacao = 5f;
    private bool estaNoChao = false;
    private Vector3 movimentacao;
    private Vector3 direcaoPulo;

    void Start()
    {
        controle = GetComponent<CharacterController>();
        animacao = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        estaNoChao = controle.isGrounded;

        movimentacao = new Vector3(horizontal * velocidadeMovimento, 0, 0);

        controle.Move(movimentacao * Time.deltaTime);
        controle.SimpleMove(Physics.gravity);

        // Rotacionando o personagem
        if (movimentacao != Vector3.zero)
        {
            animacao.SetBool("Andando", true);

            Quaternion novaRotation = Quaternion.LookRotation(movimentacao);
            transform.rotation = Quaternion.Slerp(transform.rotation, novaRotation, velocidadeRotacao * Time.deltaTime);
        }
        else
        {
            animacao.SetBool("Andando", false);
        }
        
        // Monta o Pulo
        if (estaNoChao)
        {
            if (Input.GetButtonDown("Jump"))
            {
                direcaoPulo.y = forcaPulo;                
            }
            
            animacao.SetBool("Pulando", false);
        }
        else
        {
            // Ativa a animação de pulo
            animacao.SetBool("Pulando", true);
        }

        direcaoPulo.y -= forcaGravidade * Time.deltaTime;
        controle.Move(direcaoPulo * Time.deltaTime);


    }
   
}
