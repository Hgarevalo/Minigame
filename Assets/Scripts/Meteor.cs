using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    //constantes de velocidad, tiempo de vida
    public float speed = 10f;
    public float maxLifeTime = 3f;
    //variable de direccion (a especificar en instanciacion en EnemySpawner)
    public Vector3 targetVector;

    // Start is called before the first frame update
    void Start()
    {
        //Destruye cuando pasa el tiempo especificado
        Destroy(gameObject, maxLifeTime); 
    }

    // Update is called once per frame
    void Update()
    {
        //movimiento
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

}
