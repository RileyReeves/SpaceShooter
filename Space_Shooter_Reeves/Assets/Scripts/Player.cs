 using System.Collections; //namespace Unity default
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //vairable requirements - public refrence, data type (int, float, bool, string), a name. Optional value assigned.
    [SerializeField] //allow for changing in the inspector
    private float _speed = 10f; //float is a number with a decimal and we're creating the _speed vairiable with a 2.5 value
    //create a prefab vairable for the laser
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private int _lives = 3; //setting are lives to a value of three to start.

    private SpawnManager _spawnManager;

    private UIManager _uiManager;

    void Start()  // Start is called before the first frame update
    {
        transform.position = new Vector3(0, 0, 0); //access transform component to set new position to 0, 0, 0
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }


    void Update() // Update is called once per frame
    {
        PlayerMovement();
        //if the right mouse button is pushed then the laser gets instantiated (created)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //defining the horizontal input vairable and using Unity default name.
        float verticalInput = Input.GetAxis("Vertical"); //defining the vertical input vairable and using Unity default name.

        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime); //new Vector3 (1, 0, 0) * player horizontal input * speed vairable * real time
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        //top 6 bottem -4 left -9.25 right 9.3,  if the player y vaule is greater than(>) 6 or less than(<) -9.25 the player will stop at those locations
        if (transform.position.y >= 6f)
        {
            transform.position = new Vector3(transform.position.x, 6f, 0);
        }
        else if (transform.position.y <= -4f)
        {
            transform.position = new Vector3(transform.position.x, -4f, 0);
        }
        if (transform.position.x >= 9.3f)
        {
            transform.position = new Vector3(9.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.25f)
        {
            transform.position = new Vector3(-9.25f, transform.position.y, 0);
        }
    }
    public void Damage()
     {
        _lives -= 1;
        _uiManager.UpdateLives(_lives);//links to UIManager to update the current lives

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
     }
}