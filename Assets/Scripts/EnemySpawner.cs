using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //prefab para generar asteroides
    public GameObject asteroidPrefab;
    //cuantos spawnean y el incremento de dificultad por cada instanciacion
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    private float spawnNext = 0;
    //constantes de vida y posicion de spawneo
    public float xLimit = 8;
    public float maxTimeLife = 4f;

    // Update is called once per frame
    void Update()
    {
        //si ha pasado el tiempo para spawnear otro
        if(Time.time > spawnNext)
        {
            //actualiza el tiempo de spawneo (incrementa dificultad poco a poco)
            spawnNext = Time.time + 60/spawnRatePerMinute;
            spawnRatePerMinute += spawnRateIncrement;

            //calcula posicion de meteorito a spawnear
            float rand = Random.Range(-xLimit, xLimit);
            Vector3 spawnPosition = new Vector3(rand, 8f,-1);

            //crea meteorito y le asigna su direccion  (targetVector var publica de Meteor)
            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            Meteor meteorscript = meteor.GetComponent<Meteor>();
            meteorscript.targetVector = -transform.up;

        }
    }
}
