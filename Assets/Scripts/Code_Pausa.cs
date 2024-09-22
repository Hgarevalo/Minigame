using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code_Pausa : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool Pausa = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //cuando presionas Esc
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //si no estabas pausado, pausa
            if(!Pausa)
            {
                ObjetoMenuPausa.SetActive(true);
                Pausa = true;
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            //si estabas ya pausado, resume
            else if(Pausa)
            {
                Resumir();
            }
        }
    }
    //funcion auxiliar, publica para que la pueda utilizar el boton tambien
    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        Pausa = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
