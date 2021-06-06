// all'inizio dobbiamo specificare quali librerie usare
// come vedete in grigio sono librerie che abbiamo specificato ma NON abbiamo utilizzato
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ogni programma C# definisce UNA classe, il nome minuscolo/maiuscolo deve coincidere con il nome
// del file. In questo caso il file che contiene questo testo DEVE chiamarsi Player.cs
// MonoBehaviour e' un pezzo di UnityEngine che SA come gestire le funzionalita' dei GameObject
// nel nostro esempio noi utilizzeremo le callback Start, FixedUpdate 
// useremo override quando le callback hanno gia' una implementazione in MonoBehaviour
//           nostra classe
//           v        classe di Unity Engine
//           v        v
public class Player : MonoBehaviour
{
    #region parameters
    // i parametri PUBLIC si vedranno nell'inspector
    [Header("Impostazione velocita' movimento")]
    public float speed = 10;
    [Header("Impostazione sensibilita' mouse")]
    public float mousesensitivity = 3;
    [Header("Qui dovete trascinare la camera")]
    public GameObject fpsCamera;
    #endregion
    #region variables
    private Rigidbody rb;
    // contiene l'angolo corrente di inclinazione della camera
    // che viene modificato dal mouse up and down
    private float currentCameraUpRot = 0;
    #endregion

    void Start()
    {
        // questo script deve essere messo nel gameobject in cui abbiamo messo il componente rigidbody
        // ecco perche' questa funzione riesce a trovare il rigidbody associato
        rb = this.GetComponent<Rigidbody>();
    }

   
    // FixedUpdate da usarsi per i movimenti che includono interazioni fisiche
    // Update quando solo la parte di rendering visuale e' coinvolta (no rigid body)
    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
        UpDownCamera();
    }

    // gestione del movimento con AWSD o freccie
    // W o ^ per muovere su z+ (avanti) senso positivo freccia blu
    // S o v per muovere su z- (indietro)
    // A o < per muovere su x- (sinistra)
    // D o > per muovere su x+ (destra) senso positivo freccia rossa
    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal"); // AD o <>
        float z = Input.GetAxis("Vertical"); // WS o ^v

        // applica orizzontale su destra sinistra sinistra = valori negativi
        // transform right e' il vettore "rosso" locale che tiene conto della posizione del player
        Vector3 HorizontalMovement = transform.right * x;
        // applica verticale per avanti indietro avanti = valore positivi
        // transform forward e' il vettore "blu" locale che tiene conto della posizione del player
        Vector3 VerticalMovement = transform.forward * z;

        // calcola movimento complessivo
        Vector3 movement = (HorizontalMovement + VerticalMovement);

        // solo se c'e' qualcosa da muovere
        if (movement != Vector3.zero)
        {
            // normalizza su un vettore lungo esattamente 1
            // nel caso in cui ci siano movimenti contemporanei fa in modo che sia
            // sempre lineare
            movement.Normalize();

            // applica movimento tenendo conto
            // * della velocita' impostata nell'inspector
            // * del fixedDeltaTime che serve a "normalizzare" il movimento in modo da renderlo fluido.
            rb.MovePosition(rb.transform.position + movement * Time.fixedDeltaTime * speed);
        }

        
    }

    // gestione della rotazione del giocatore (mouse sinistra / destra)
    private void RotatePlayer()
    {
        // ottiene il valore di scostamento del mouse - per sx + per destra
        float mx = Input.GetAxis("Mouse X");

        // la rotazione deve essere applicata all'asse Y verticale il secondo numero
        // mouse sensitivity serve per poter controllare la velocita' di rotazione del player
        Vector3 rotation = new Vector3(0, mx, 0) * mousesensitivity;

        // in matematica aggiungere una rotazione ad una rotazione gia' presente
        // [ le rotazioni vengono rappresentate da Quaternioni, ma noi usiamo una rappresentazione
        // detta "di Eulero" che e' piu' semplice in cui si indicano i gradi di rotazione attorno 
        // agli assi X, Y, Z dove Y e' la rotazione che ci interessa.
        // dobbiamo quindi convertire il valore precedente rotation che era espresso in termini
        // di Eulero in modo che sia una rotazione valida. Ecco perche' lo trasformiamo in Quaternione]
        rb.MoveRotation(rb.transform.rotation * Quaternion.Euler(rotation));

    }

    // gestiamo la rotazione che in termine tecnico si chiama "tilt" che indica la pendenza
    // usiamo il mouse lungo la direzione verticale in alto alza la telecamera = riduce il tilt
    // per questo motivo mettiamo -= updown
    // ci memorizziamo un angolo di partenza currentCameraUpRot che poi andremo a modificare
    // ottenendo l'angolo Euleriano
    private void UpDownCamera()
    {
        float my = Input.GetAxis("Mouse Y");
        float updown = my * mousesensitivity;
        // computa il nuovo angolo invertendo il valore di Y 
        currentCameraUpRot -= updown;

        // fortunatamente possiamo impostare l'angolo della camera usando un semplice vettore Euleriano
        // notate che ruotiamo la camera girandola attorno al suo asse X rosso
        fpsCamera.transform.localEulerAngles = new Vector3(currentCameraUpRot, 0, 0);
    }
}
