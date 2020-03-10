using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class placeAndUI : MonoBehaviour
{

    public Camera cam;
    [Space]
    [Header("Tower and Texture Array must have same size AND lower 10")]
    [Space]
    public GameObject[] tower;                   //Prefab Array for towers  
    public GameObject[] towerTrue;
    public GameObject[] towerFalse;
    public int[] price;
    public Texture[] texture;                    //Images for Toolbar
    public RawImage[] image = new RawImage[9];   //Toolbar Objects
    public Text templeHealth;
    public Text money;
    public Text wave;
    RawImage tempImg;                       //shorter spelling
    [Space(10)]
    public float placeRange = 20;           //Max distance to place tower
    public LayerMask LayerM;                //for Raycast--> see LayerMask
    public GameObject ui;
     
    int keyValue = 1;                       //last accepted pressed number Key
    int newKeyValue = 1;                    //last pressed number Key
    int changeCount = 0;                    //Key pressed actualisation 
    int oldChangeCount = 0;                 //test value

    GameObject towerObject;
    [HideInInspector]
    public bool onlyGroundCollision = true; //test place statements: false if collider not Build and Ground 
    [HideInInspector]
    public bool buildMode = false;          //if B pressed change from false to true and contrawise
    bool objectSelected = false;            //if true tower reference is same 
    float buildModeDelay;                   //Delay for controlled Mode change 
    bool setTower;                          //if Mouse0 ( Left ) is pressed seting is available
    float setTowerDelay;                    //Delay for controlled Tower placing 



    // Start is called before the first frame update
    void Start()
    {
        if ((tower.Length != texture.Length || tower.Length != towerTrue.Length || tower.Length != towerFalse.Length || price.Length != towerFalse.Length) && tower.Length < 10)             //Test for basic value conditions
        {
            Debug.LogError("ERROR: Towers and Texture Arrays must have same size AND lower 10");
            this.GetComponent<placeAndUI>().enabled = false;                 //not accepted deactivate this Script for protection
        }
        else
        {
            for (int i = texture.Length ; i< image.Length;i++)               //deactivate not used Toolbar components
            {
                image[i].enabled = false;
            }

            for (int i = 0; i < texture.Length;i++)                          //set alpha of allowed Toolbar components
            {
                tempImg = image[i];
                tempImg.texture = texture[i];
                tempImg.GetComponent<RawImage>().color = 
                    new Color(tempImg.color.r, tempImg.color.g, tempImg.color.b, 0.4f);
            }

            tempImg= image[keyValue - 1];                                    //set starting conditions
            image[keyValue - 1].rectTransform.anchoredPosition += new Vector2(0, 8);
            image[keyValue - 1].GetComponent<RawImage>().color =
                    new Color(tempImg.color.r, tempImg.color.g, tempImg.color.b, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PressedKey();                                             //Key actualisation

        //Create Ray with condition of hit sth 
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, placeRange, LayerM))
        {
            if (buildMode == true)                                //Activated buildMode
            {
                if (objectSelected == false)                      //actualisation if sth changed: Keyvalue, Visibility, buildMode                           
                {
                    
                    DestroyTower(towerObject);

                    //Instatiate Tower with KeyValue if "hit" is a Ground/Tower/Wall
                    if (hit.transform.tag == "Ground" || hit.transform.tag == "Tower" || hit.transform.tag == "Wall" || hit.transform.tag == "Untagged")
                    {
                        towerObject = Instantiate(towerTrue[keyValue - 1], hit.point, Quaternion.Euler(0, 0, 0));
                        towerObject.GetComponent<towerPlaceScript>().player = this.gameObject;
                        objectSelected = true;                   //selected tower is ready
                    }
                }
                else
                {
                    towerObject.transform.position = hit.point;    //actualisate to place tower position

                    //collision arguments from towerPlaceScript: only allowed collisions make tower set able
                    if(onlyGroundCollision == true && GetComponent<CoinSystem>().money >= price[keyValue - 1])  
                    {
                        //Debug.Log(true);
                        towerObject.GetComponent<towerPlaceScript>().destroy = true;
                        towerObject = Instantiate(towerTrue[keyValue - 1], hit.point, Quaternion.Euler(0, 0, 0));
                        towerObject.GetComponent<towerPlaceScript>().player = this.gameObject;

                        if (setTower == true && Time.time >= setTowerDelay)               //Instatiate final Tower
                        {
                            //Debug.Log("PlaceObject");
                            towerObject.GetComponent<towerPlaceScript>().destroy = true;
                            Instantiate(tower[keyValue - 1], hit.point, Quaternion.Euler(0, 0, 0));
                            setTowerDelay = Time.time + 1;                               //Delay for Control
                            GetComponent<CoinSystem>().buy(price[keyValue - 1]);
                        }  
                    }
                    else
                    {
                        //Debug.Log(false);
                        towerObject.GetComponent<towerPlaceScript>().destroy = true;
                        towerObject = Instantiate(towerFalse[keyValue - 1], hit.point, Quaternion.Euler(0, 0, 0));
                        towerObject.GetComponent<towerPlaceScript>().player = this.gameObject;
                    }
                }
            }
            else
            {
                DestroyTower(towerObject);                         //by no buildMode destroy selected tower
            }
        }
        else
        {
            DestroyTower(towerObject);                             //by no hit destroy selected tower
            objectSelected = false;
        }
        
        
        UserInterface();              
        //PlaceObject(hit);
    }

    void PlaceObject(RaycastHit hit)
    {

    }

    void DestroyTower(GameObject go)
    {
        try                                  //else there are multiple exceptions            
        {
            if (towerObject.tag == "Build")  //for savety 
            {   
                go.GetComponent<towerPlaceScript>().destroy = true;    //change value of GameO do destroy it self
            }
        }
        catch (System.Exception)                   
        { } 
    }

    void UserInterface()
    {
        // efficent UI actualisation only by changed key value 
        if (newKeyValue <= tower.Length && changeCount != oldChangeCount && newKeyValue != keyValue)
        {
            tempImg = image[keyValue - 1];
            tempImg.rectTransform.anchoredPosition += new Vector2(0, -8);               //change position/color back of old Toolbar Object
            tempImg.GetComponent<RawImage>().color = 
                new Color(tempImg.color.r, tempImg.color.g, tempImg.color.b, 0.4f);

            tempImg = image[newKeyValue - 1];

            tempImg.rectTransform.anchoredPosition += new Vector2(0, 8);                //set position/color new selected Toolbar Object
            tempImg.GetComponent<RawImage>().color = 
                new Color(tempImg.color.r, tempImg.color.g, tempImg.color.b, 1);

            objectSelected = false;                                                     //show place system change
            keyValue = newKeyValue;                                                     //set new Key Value
            oldChangeCount = changeCount = 0;                                           //reset counter
        }

        money.GetComponent<Text>().text = GetComponent<CoinSystem>().money.ToString() +"$";
        wave.GetComponent<Text>().text = "Wave: " + GetComponent<Wave>().wave.ToString();
        templeHealth.GetComponent<Text>().text = GameObject.Find("Temple").GetComponent<Temple>().health.ToString();
    }

    void PressedKey()
    {
        if (Input.GetKey(KeyCode.Alpha1) == true)            //Get Key Input
        {
            newKeyValue = 1;                                 //set new Key Value
            changeCount += 1;                                //detect change in keys
        }
        else if (Input.GetKey(KeyCode.Alpha2) == true)
        {
            newKeyValue = 2;
            changeCount += 1;
        }
        else if (Input.GetKey(KeyCode.Alpha3) == true)
        {
            newKeyValue = 3;
            changeCount += 1;
        }
        else if (Input.GetKey(KeyCode.Alpha4) == true)
        {
            newKeyValue = 4;
            changeCount += 1;
        }
        else if (Input.GetKey(KeyCode.Alpha5) == true)
        {
            newKeyValue = 5;
            changeCount += 1;
        }
        else if (Input.GetKey(KeyCode.Alpha6) == true)
        {
            newKeyValue = 6;
            changeCount += 1;
        }
        else if (Input.GetKey(KeyCode.Alpha7) == true)
        {
            newKeyValue = 7;
            changeCount += 1;
        }
        else if (Input.GetKey(KeyCode.Alpha8) == true)
        {
            newKeyValue = 8;
            changeCount += 1;
        }
        else if (Input.GetKey(KeyCode.Alpha9) == true)
        {
            newKeyValue = 9;
            changeCount += 1;
        }

        if (Input.GetKey(KeyCode.Mouse1) == true && Time.time > buildModeDelay)        //Key Delay added for controled mode change
        {
            if (buildMode == false)                           
            {
                buildMode = true;
                objectSelected = false;                    // by press B:
            }                                              // false--> true // true --> false
            else
            {
                buildMode = false;
                objectSelected = false;
            }

            buildModeDelay = Time.time + 1;                //set Delay
        }

        if (Input.GetKey(KeyCode.Mouse0) == true)
        {
            setTower = true;
        }
        else
        {
            setTower = false;
        }
    }

    private void OnDisable()
    {
        DestroyTower(towerObject);
        buildMode = false;
        ui.SetActive(false);
    }
    private void OnEnable()
    {
        ui.SetActive(true);
    }
}
