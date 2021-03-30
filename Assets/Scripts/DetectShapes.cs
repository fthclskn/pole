using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectShapes : MonoBehaviour
{
    public int pointsAdded = 0,pointsToCollect=0,pointsLeftAdded=0,pointsRightAdded=0,pointsDownAdded=0,HeliUpAdded=0;
    public List<PointsScript> points = new List<PointsScript>();
    [SerializeField]
    private Transform pointsUPParent,pointsLeftParent,pointsRightParent,pointsDownParent,HeliUpParent;
    public LineRenderer LinePrefab;
    public int lineLength = 0,totalLineLength;
    public GameObject rightPart;
    public Animator anim;
    public bool isRunnig =true;
    public GameObject Player;
    public Rigidbody PlayerRigidbody;
    private bool ToLeft =false;
    private bool ToRight=false;
    private bool ToDown=false;
    private bool IsPoleAnim=false;
    public float HorizontalVelocity,ZaxisVelocity;
    public static DetectShapes instance;
    public GameObject[] ClosedPoints;
    public GameObject box;
    Color[] colors = new Color[] {Color.red, new Color(1f, 0.3f, 0.04f), Color.blue, Color.green};
    private int currentColor,length;
    
    

   
    public float verticalVelocity;
    private float gravity = 11f;
    private float jumpForce = 300.0f;
    private bool isPeak;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        anim =GameObject.Find("untitled").GetComponent<Animator>();
        
       
        foreach (Transform P in pointsUPParent)
        {
            points.Add(P.GetComponent<PointsScript>());
        }
        foreach (Transform P in pointsLeftParent)
        {
            points.Add(P.GetComponent<PointsScript>());
        }
        foreach (Transform P in pointsRightParent)
        {
            points.Add(P.GetComponent<PointsScript>());
        }
        foreach (Transform P in pointsDownParent)
        {
            points.Add(P.GetComponent<PointsScript>());
        }
        foreach (Transform P in HeliUpParent)
        {
            points.Add(P.GetComponent<PointsScript>());
        }
        
        currentColor = 0; //White
        length = colors.Length;
        box.GetComponent<MeshRenderer>().material.color = colors [currentColor];
        
        
    }


    void FixedUpdate()
    {
        
/*        if (EnergySystem.instance.currentEnergy==0)
        {
            //todo Lose UI
            //todo Lose Anim
            ZaxisVelocity = 0f;
            isRunnig = false;
        }*/
        rayMethod(Input.mousePosition);
        if (isRunnig)
        {
            anim.SetBool("Pole",false);
            anim.SetBool("PoleLeft",false);
            anim.SetBool("PoleRight",false);
            anim.SetBool("Slide",false);
            anim.SetBool("Roll", false);
            verticalVelocity = 0f;
            HorizontalVelocity = 0f;
            ZaxisVelocity = 5f;
        }
        
        else if (!isRunnig)
        {
            if (IsPoleAnim)
            {
                verticalVelocity += gravity * Time.deltaTime;
                if (!isPeak)
                {
                    verticalVelocity = -jumpForce * Time.deltaTime;
                
                }
                if (ToLeft)
                {
                    HorizontalVelocity += -7f * Time.deltaTime;
                }

                if (ToRight)
                {
                    HorizontalVelocity += 7f * Time.deltaTime;
                }
            }
            if (ToDown)
            {
                ZaxisVelocity = 7f;
            }
            
            
        }
        
        
        
        
    }



    void rayMethod(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
       

        if (Input.GetMouseButton(0))
        {
            
            if (hit.collider&&hit.collider.CompareTag("UP"))
            {
                PointsScript pS = hit.transform.GetComponent<PointsScript>();
                if (!pS.isTouched)
                {
                    pS.isTouched = true;
                    print(hit.collider.name);
                    pointsAdded++;
                    PoleManager.instance.GrowPole();


                }
            }
            if (hit.collider&&hit.collider.CompareTag("LEFT"))
            {
                PointsScript pS = hit.transform.GetComponent<PointsScript>();
                if (!pS.isTouched)
                {
                    pS.isTouched = true;
                    print(hit.collider.name);
                    pointsLeftAdded++;
                    PoleManager.instance.GrowPole();


                }
            }

            if (hit.collider&&hit.collider.CompareTag("RIGHT"))
            {
                PointsScript pS = hit.transform.GetComponent<PointsScript>();
                if (!pS.isTouched)
                {
                    pS.isTouched = true;
                    print(hit.collider.name);
                    pointsRightAdded++;
                    PoleManager.instance.GrowPole();

                   

                }
            }
            if (hit.collider&&hit.collider.CompareTag("DOWN"))
            {
                PointsScript pS = hit.transform.GetComponent<PointsScript>();
                if (!pS.isTouched)
                {
                    pS.isTouched = true;
                    print(hit.collider.name);
                    pointsDownAdded++;
                    

                }
            }
            if (hit.collider&&hit.collider.CompareTag("HeliUp"))
            {
               CodeLock codelock  = hit.transform.gameObject.GetComponentInParent<CodeLock>();
              // Animation animq = cubes.GetComponent<Animation>();
                //if (!pS.isTouched)
               // {
                    //pS.isTouched = true;
                    print(hit.collider.name);
                if (codelock !=null)
                {
                    currentColor = (currentColor+1)%length;
                    box.GetComponent<MeshRenderer>().material.color = colors[currentColor];
                    //animq.Play("Cube_CubeAction");
                    //hit.transform.gameObject.SetActive(false);
                    hit.collider.enabled = false;
                    string value = hit.transform.name;
                    codelock.SetValue(value);

                }
                    //HeliUpAdded++;
                    

               // }
            }

            
        }

        
        
    }

    public void CheckPointMethod()
    {
        //Codelock();
        if (pointsAdded < pointsToCollect || lineLength > totalLineLength )
        {
            pointsAdded = 0;
            lineLength = 0;
            for (int i = 0; i < points.Count; i++)
            {
                points[i].isTouched = false;
               
            }
        }
        else if (pointsAdded > pointsToCollect || lineLength < totalLineLength)
        {
            pointsAdded = 0;
            lineLength = 0;
           // isPeak = true;
           // IsPoleAnim = true;
           // StartCoroutine(delayForPeak());
           // isRunnig = false;
           // StartCoroutine(delayForPoleAnim());
           // anim.SetBool("Pole",true);
           PoleManager.instance.InitiateJumpUp();
           // rightPart.SetActive(true);
            //rightPart.GetComponent<SkinnedMeshRenderer>().enabled = true;
            StartCoroutine(GameManager.Instance.GameCompleteMethod());
           // pointsParent.gameObject.SetActive(false);
           //EnergySystem.instance.UseEnergy(10);
        }
        if (pointsLeftAdded < pointsToCollect || lineLength > totalLineLength )
        {
            pointsLeftAdded = 0;
            lineLength = 0;
            for (int i = 0; i < points.Count; i++)
            {
                points[i].isTouched = false;
               
            }
        }
        else if (pointsLeftAdded > pointsToCollect || lineLength < totalLineLength)
        {
            pointsLeftAdded = 0;
            lineLength = 0;
           // IsPoleAnim = true;
            //ToLeft = true;
            //isPeak = true;
           // StartCoroutine(delayForPeak());
           // isRunnig = false; 
           // StartCoroutine(delayForPoleAnim());
            //anim.SetBool("PoleLeft",true);
           // rightPart.SetActive(true);
           PoleManager.instance.InitiateJumpLeft();

            
           // anim.SetBool("WallRun",true);
            StartCoroutine(GameManager.Instance.GameCompleteMethod());
           // EnergySystem.instance.UseEnergy(10);
            
        }
        if (pointsRightAdded < pointsToCollect || lineLength > totalLineLength )
        {
            pointsRightAdded = 0;
            lineLength = 0;
            for (int i = 0; i < points.Count; i++)
            {
                points[i].isTouched = false;
               
            }
        }
        else if (pointsRightAdded > pointsToCollect || lineLength < totalLineLength)
        {
            pointsRightAdded = 0;
            lineLength = 0;
            PoleManager.instance.InitiateJumpRight();
            // IsPoleAnim = true;
          //  isPeak = true;
          //  ToRight = true;
           // StartCoroutine(delayForPeak());
          //  isRunnig = false;
           // StartCoroutine(delayForPoleAnim());
          //  anim.SetBool("PoleRight",true);
          //  rightPart.SetActive(true);
            //rightPart.GetComponent<SkinnedMeshRenderer>().enabled = true;
            StartCoroutine(GameManager.Instance.GameCompleteMethod());
            // pointsParent.gameObject.SetActive(false);
            //EnergySystem.instance.UseEnergy(10);
        }
        if (pointsDownAdded < pointsToCollect || lineLength > totalLineLength )
        {
            pointsDownAdded = 0;
            lineLength = 0;
            for (int i = 0; i < points.Count; i++)
            {
                points[i].isTouched = false;
               
            }
        }
        else if (pointsDownAdded > pointsToCollect || lineLength < totalLineLength)
        {
            pointsDownAdded = 0;
            lineLength = 0;
            ToDown = true;
            // isPeak = true;
            //ToRight = true;
            //StartCoroutine(delayForPeak());
            isRunnig = false;
            StartCoroutine(delayForWallRunAnim());
            anim.SetBool("Slide",true);
            //rightPart.SetActive(true);
            //rightPart.GetComponent<SkinnedMeshRenderer>().enabled = true;
            StartCoroutine(GameManager.Instance.GameCompleteMethod());
            // pointsParent.gameObject.SetActive(false);
            EnergySystem.instance.UseEnergy(10);
        }
       /* if (HeliUpAdded < pointsToCollect )
        {
            HeliUpAdded = 0;
            lineLength = 0;
            for (int i = 0; i < points.Count; i++)
            {
                points[i].isTouched = false;
                
               
            }
            foreach (var x in ClosedPoints)
            {
                x.SetActive(true);
            }

            CodeLock.instance.attemtedCode = "";
        }
        /*else if (HeliUpAdded > pointsToCollect)
        {
            HeliUpAdded = 0;
            lineLength = 0;
           // GameObject.Find("küp").GetComponent<Animator>().SetBool("a",true);
           // IsPoleAnim = true;
            //isPeak = true;
            //ToRight = true;
           // StartCoroutine(delayForPeak());
            //isRunnig = false;
            //StartCoroutine(delayForPoleAnim());
            //anim.SetBool("PoleRight",true);
            //rightPart.SetActive(true);
            //rightPart.GetComponent<SkinnedMeshRenderer>().enabled = true;
            StartCoroutine(GameManager.Instance.GameCompleteMethod());
            // pointsParent.gameObject.SetActive(false);
            EnergySystem.instance.UseEnergy(10);
            foreach (var x in ClosedPoints)
            {
                x.SetActive(true);
            }
            CodeLock.instance.attemtedCode = "";
            
        }*/
       
    }

    /*void Codelock()
    {
        foreach (var x in CodeLock.instance.array)
        {
            x.SetActive(true);
        }

        //CodeLock.instance.attemtedCode = "";
        //CodeLock.instance.placeInCode = 0;
        CodeLock.instance.CheckCode();
    }*/
    IEnumerator delayForWallRunAnim()
    {
        yield return new WaitForSeconds(1.15f);
        isRunnig = true;
        

    }

    IEnumerator delayForPoleAnim()
    {
        yield return new WaitForSeconds(2.043f);
        isRunnig = true;
        rightPart.SetActive(false);
        IsPoleAnim = false;
        ToLeft = false;
        ToRight = false;
        
    }

    IEnumerator delayForPeak()
    {
        yield return new WaitForSeconds(1.024f);
        isPeak = false;
        
    }
}
