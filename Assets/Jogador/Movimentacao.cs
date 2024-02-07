using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private CharacterController controle;
    private float horizontal;
    private Vector3 movimentacao;
    
    public float velocidadeMovimento;
    public float velocidadeRotacao;
    
    void Start()
    {
        controle = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
        Rotacao();
    }

    private void Movimento(){
        horizontal = Input.GetAxis("Horizontal");
        movimentacao = new Vector3(horizontal * velocidadeMovimento, 0, 0);
        controle.Move(movimentacao * Time.deltaTime);
    }

    private void Rotacao(){
        if(movimentacao != Vector3.zero){
            Quaternion novaRotacao = Quaternion.LookRotation(movimentacao);
            transform.rotation = Quaternion.Slerp(transform.rotation, novaRotacao, velocidadeRotacao * Time.deltaTime);
        }
    }
}
