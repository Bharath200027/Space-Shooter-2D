using System.Collections;
using System.Collections.Generic;
using mixpanel;
using UnityEngine;
using System.IO;
using GameAnalyticsSDK;



public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float horizontalInput;
    private float verticalInput;


    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1.0f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private GameObject _rightEngine, _leftEngine;


    [SerializeField]
    private int _score;

    private GameManager _gameManager;

    private SpawnManager _spawnManager;

    private bool _isCoopMode;

    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private bool _isShieldsActive = false;

    private UI_Manager _uiManager;

    [SerializeField]
    private AudioClip _laserSoundClip;

    
    private AudioSource _audioSource;

    string path;
    string jsonString;
    string progression01;
    string progression02;
    string eventName;
    string eventValue;
    string message;



    void Start()
    {
       
        GameAnalytics.Initialize();
        GameAnalytics.SetCustomId("user1234567879");
        GameAnalytics.SetEnabledManualSessionHandling(true);

        //Dictionary<string, object> fields = new Dictionary<string, object>();
        //fields.put("test", 100);
        //fields.put("test_2", "hello_world");

        //GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.Banner, "admob", "[AD_PLACEMENT_OR_UNIT_ID], fields");


        //GameAnalytics.NewErrorEvent(GA_Error.GAErrorSeverity severity message);




       // Mixpanel.Track("Sent Message");
       //Mixpanel.Track("The Game data is updated");

       ////Timed Events using Mixpanel
       //Mixpanel.StartTimedEvent("Image Upload");
       //Mixpanel.Track("Image Upload");

       ////Sending first event values.

       //var props = new Value();
       //props["Plan"] = "Updated the Game";
       //Mixpanel.Track("Plan Selected", props);

       ////Register
       //Mixpanel.Register("User Type", "Paid");

       //props["signup_button"] = "test12";
       //props["User Type"] = "Paid";


       



        //Mixpanel.Track("signup", props);

        //Stringify JSON data to a message
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _audioSource = GetComponent<AudioSource>();

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
         
        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

        if(_audioSource == null)
        {
            Debug.LogError("The Audio Source is NULL");
        }

        else
        {
            _audioSource.clip = _laserSoundClip;
        }

        //if (_gameManager._isCoopMode == false)
        //{
        //    transform.position = new Vector3(0, 0, 0);
        //}

    }


    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) & Time.time > _canFire)
        {
        FireLaser();
        }        
    }

    //public void LostLevel(int curLevel)
    //{
    //    GameAnalytics.NewDesignEvent("levelJump", + curLevel, level.JumpAmnt);
    //}
    void CalculateMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.7f, 0), 0);

        if (transform.position.x >= 4.5f)
        {
            transform.position = new Vector3(-4.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -4.5f)
        {
            transform.position = new Vector3(4.5f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        _audioSource.Play();
        
    }

    public void Damage()
    {
        _lives--;

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }

        else if (_lives == 1)
        {
           // _leftEngine.SetActive(true);
            _rightEngine.SetActive(true);
        }
        _uiManager.UpdateLives(_lives);

        if(_isShieldsActive == true)
        {
            _isShieldsActive = false;
            return;
        }

        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            Debug.Log("The player has been defeated. The game ends now");
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    public void ShieldsActive()
    {
        //yield return new WaitForSeconds(3.0f);
        _isShieldsActive = true;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);


    }
}
