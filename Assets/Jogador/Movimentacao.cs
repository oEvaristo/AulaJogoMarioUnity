using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private CharacterController controle;
    private Animator animacao;
    private bool estaNoChao = true;
    private Vector3 movimentacao;
    private Vector3 direcaoPulo;
    private float horizontal;

    public float velocidadeMovimento = 10f;
    public float gravidade = 9.8f;
    public float forcaPulo = 2f;
    public float velocidadeRotacao = 5f;

    void Start()
    {
        controle = GetComponent<CharacterController>();
        animacao = GetComponent<Animator>();
    }

    void Update()
    {
        Movimento();
        Rotacao();
        Pulo();
    }

    private void Movimento()
    {
        horizontal = Input.GetAxis("Horizontal");
        estaNoChao = controle.isGrounded;
        movimentacao = new Vector3(horizontal * velocidadeMovimento, 0, 0);
        controle.Move(movimentacao * Time.deltaTime);

        // Gravidade
        direcaoPulo.y -= gravidade * Time.deltaTime;
    }

    private void Rotacao()
    {
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
    }

    private void Pulo()
    {

        if (estaNoChao)
        {
            direcaoPulo.y = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                direcaoPulo.y = forcaPulo;
            }

            animacao.SetBool("Pulando", false);
        }
        else
        {
            animacao.SetBool("Pulando", true);
        }

        controle.Move(direcaoPulo * Time.deltaTime);
    }
}