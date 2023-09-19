using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables publicas
    public Transform cameraAim;
    public float walkSpeed, runSpeed, rotationSpeed;
    public bool canMove;

    // Variables privadas
    private Vector3 vectorMovement;
    private float speed;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializacion de variables
        characterController = GetComponent<CharacterController>();
        speed = walkSpeed;
        vectorMovement = Vector3.zero;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            Walk();
            Run();
            AlignPlayer();
        }
        Gravity();
    }

    //Funcion para caminar
    void Walk()
    {
        // Conseguimos los inputs
        vectorMovement.x = Input.GetAxis("Horizontal");
        vectorMovement.z = Input.GetAxis("Vertical");

        // Normalizamos el vector de movimiento
        vectorMovement = vectorMovement.normalized;

        // Nos movemos en direccion a la camara
        vectorMovement = cameraAim.TransformDirection(vectorMovement);

        // Movemos al player
        characterController.Move(vectorMovement * speed * Time.deltaTime);
    }

    // Funcion para correr
    void Run()
    {
        // Si presionamos el boton para correr modificamos la velocidad
        if(Input.GetAxis("Run") > 0f)
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }

    // Funcion provicional de gravedad
    void Gravity()
    {
        characterController.Move(new Vector3(0f, -4f * Time.deltaTime, 0f));
    }

    // Funcion para alinear al jugador hacia donde se mueve
    void AlignPlayer()
    {
        if(characterController.velocity.magnitude > 0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vectorMovement), rotationSpeed * Time.deltaTime);
        }
    }
}
