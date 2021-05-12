using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IColorized
{

    [Tooltip("Translation Speedin m.s-1")]
    [SerializeField] float m_TranslationSpeed;

    [Tooltip("Translation speed in Degre s-1")]
    [SerializeField] float m_RotationSpeed;

    [Header("Ball Setup")]
    [SerializeField] GameObject m_BallPrefab;
    [SerializeField] float m_BallStartSpeed;
    [SerializeField] Transform m_BallSpawnPos;

    [SerializeField] float m_BallLifeDuration;

    [SerializeField] float m_BallCoolDownDuration;
    float m_BallNextShootTime;
    Transform m_Transform;    // Start is called before the first frame update
    Rigidbody m_Rigidbody;

    Vector3 m_PreviousPosition;
    Vector3 m_Velocity;
    private void Awake() {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody =  GetComponent<Rigidbody>(); //transform
    }
    
    void Start()
    {
        m_BallNextShootTime = Time.time;
        m_PreviousPosition = m_Transform.position;

    }

    // Comportement cinématique : Update(), transform, deltaTime ...
    // Les positions & orientations de l'objet sont 
    // Update is called once per frame
    void Update()
    {
    //     float vInput = Input.GetAxis("Vertical");
    //     float hInput = Input.GetAxis("Horizontal");

    //     // Debug.Log("vInput = " + vInput);

    //     //TRANSLATION

    //     //Vector3 moveVect = vInput * m_Transform.forward * m_TranslationSpeed * Time.deltaTime;
    //     //transform.position += moveVect;
    //     //m_Transform.Translate(moveVect, Space.World);

    //     Vector3 moveVect = vInput * Vector3.forward * m_TranslationSpeed * Time.deltaTime;
    //     m_Transform.Translate(moveVect, Space.Self);

    //     //ROTATION

    //     float deltaAngle = hInput * m_RotationSpeed * Time.deltaTime;
    //     //m_Transform.Rotate(m_Transform, deltaAngle, Space.World);
    //     m_Transform.Rotate(Vector3.up, deltaAngle, Space.Self);
        Debug.DrawLine(m_Transform.position + Vector3.up, m_Transform.position + Vector3.up + m_Velocity, Color.red);
    }

    //Comportement cinétique (physique): FixeUpdate(), rigibody, fixedDeltaTime ...
    // Les positions & orientations de l'objet seront calculées par le moteur physique d'Unity3D

    private void FixedUpdate() {
        // MovePosition & MoveRotation
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        bool fire1 = Input.GetButton("Fire1");
        
    
        Vector3 moveVect = vInput * m_Transform.forward * m_TranslationSpeed * Time.deltaTime;
        Vector3 newPos = m_Rigidbody.position + moveVect;
        m_Rigidbody.MovePosition(newPos);

        //Quaternion de redressement -> Je fais coincider le up de l'objet avec le up du monde
        Quaternion upRightQ = Quaternion.FromToRotation(m_Transform.up, Vector3.up);

        //Nouvelle orientation prise par le player après input
        float deltaAngle = hInput * m_RotationSpeed * Time.deltaTime;
        Quaternion newOrientation = upRightQ * Quaternion.AngleAxis(deltaAngle, m_Transform.up)*m_Rigidbody.rotation;

        //Concaténation des 2 quaternions précédents ... avec une interpolation .. ça permet de faire tendre doucement l'orientation de l'objet vers l'orientation désiré. 
        Quaternion targetQ = Quaternion.Slerp(newOrientation, upRightQ * newOrientation, 4 * Time.fixedDeltaTime);
            
        m_Rigidbody.MoveRotation(targetQ);
        m_Rigidbody.AddForce(- m_Rigidbody.velocity,ForceMode.VelocityChange);
        m_Rigidbody.AddTorque(- m_Rigidbody.angularVelocity,ForceMode.VelocityChange);

        /*
        // AddForce & AddTorque

        Vector3 newVelocity = vInput * m_Transform.forward * m_TranslationSpeed;
        m_Rigidbody.AddForce(newVelocity - m_Rigidbody.velocity,ForceMode.VelocityChange);

        Vector3 newRotationVector = hInput * m_Transform.up * m_RotationSpeed;
        m_Rigidbody.AddTorque( newRotationVector - m_Rigidbody.angularVelocity,ForceMode.VelocityChange);

        Quaternion upRightQ = Quaternion.FromToRotation(m_Transform.up, Vector3.up);
        Quaternion targetQ = Quaternion.Slerp(m_Rigidbody.rotation, upRightQ * m_Rigidbody.rotation, 4 * Time.fixedDeltaTime);
        m_Rigidbody.rotation = targetQ;
        */

        m_Velocity = (m_Rigidbody.position - m_PreviousPosition) / Time.fixedDeltaTime;
        m_PreviousPosition = m_Rigidbody.position;

        if (fire1 && Time.time > m_BallNextShootTime)
        {
            ShootBall();
            m_BallNextShootTime = Time.time + m_BallCoolDownDuration;
        }
    }

    void ShootBall () {
        GameObject newBallGo = Instantiate(m_BallPrefab);
        newBallGo.transform.position = m_BallSpawnPos.position;
        
        newBallGo.GetComponent<Rigidbody>().velocity = m_BallSpawnPos.forward * m_BallStartSpeed;

        Destroy(newBallGo, m_BallLifeDuration);
    }

    public void Colorized() {
        MyTools.ChangeColorRandom(gameObject);
    }
}

