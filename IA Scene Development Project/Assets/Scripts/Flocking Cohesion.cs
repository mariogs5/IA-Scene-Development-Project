using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingCohesion : MonoBehaviour
{
    float speed;

    void Start()
    {
        speed = Random.Range(FlockingManager.FM.minspeed, FlockingManager.FM.maxspeed);
    }

    void Update()
    {
        if (Random.Range(0, 100) < 10)
        {
            speed = Random.Range(FlockingManager.FM.minspeed, FlockingManager.FM.maxspeed);
        }

        if (Random.Range(0, 100) < 50)
        {
            Rules();
        }
            
        this.transform.Translate(0, 0, speed * Time.deltaTime);

        if (this.transform.position.y <= 1)
        {
            this.transform.Translate(0, speed * Time.deltaTime, 0);
        }
    }

    void Rules()
    {
        GameObject[] gameobjects;
        gameobjects = FlockingManager.FM.bees;
        Vector3 center = Vector3.zero;
        Vector3 avoid = Vector3.zero;

        int size = 0;
        float groupSpeed = 0.01f;
        float neighbourDistance = 0;

        foreach (GameObject gameobj in gameobjects) //Iterar cada uno de los GameObjects en la lista de GameObjects
        {
            if (gameobj != this.gameObject) //Mirar si el GameObject que vamos a comparar no es el mismo que estamos usando
            {
                neighbourDistance = Vector3.Distance(gameobj.transform.position, this.transform.position); //Calcular la distancia entre los dos GameObject

                if (neighbourDistance <= FlockingManager.FM.neighbourDistance) //Comprobar la distancia de los GameObject (Si estan cerca se añaden al grupo junto al centro)
                {
                    center += gameobj.transform.position;
                    size++;

                    if (neighbourDistance < 1.0f) //Comprobar si estan demasiado cerca se alejan para no chocar
                    {
                        avoid = avoid + (this.transform.position - gameobj.transform.position);
                    }

                    FlockingCohesion anotherFlock = gameobj.GetComponent<FlockingCohesion>(); //Para poder obtener las propiedades del GameObject que esta siendo iterado en concreto
                    groupSpeed = groupSpeed + anotherFlock.speed; //Sumar la velocidad del grupo con la del GameObject iterado
                }
            }
        }
        if (size > 0) //Comprobar si se ha creado un grupo (mas de un GameObject cerca)
        {
            center = center / size + (FlockingManager.FM.WhereToGo - this.transform.position); //Redefinir el centro a razon del tamaño del grupo y cambiar la posicion a la que se quiere llegar
            speed = groupSpeed / size; //Redefinir la velocidad a razon del tamaño del grupo

            if (speed > FlockingManager.FM.maxspeed)
            {
                speed = FlockingManager.FM.maxspeed;
            }

            Vector3 direction = (center + avoid) - transform.position; //Generar un vector direccion teniendo en cuenta el centro del grupo, la posicion de los otros miembros del grupo y la direccion de ese GameObject

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), FlockingManager.FM.rotationSpeed * Time.deltaTime); //Rotar el GameObject hacia la direccion que necesita para no chocarse
            }
        }
    }
}
