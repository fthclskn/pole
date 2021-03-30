using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerView : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] Material green;
    [SerializeField] Material red;
    [SerializeField] Material blue;
    [SerializeField] Material yellow;
    public GameObject CFX;
    public float verticalVelocity;
    [SerializeField]
    private float JumpSpeed;
    private PlayerMovement movement;
   
    

    public string currentPlayerColor;
    private void Start()
    {
        movement=GameObject.Find("untitled").GetComponent<PlayerMovement>();
        ChangePlayerColor();

    }

    

    public void ChangePlayerColor(string playerColor = null)
    {
        if (playerColor != null)
        {
            currentPlayerColor = playerColor;
        }
        else
        {
            int randomValue = UnityEngine.Random.Range(0, 3);

            if(randomValue == 0)
            {
                currentPlayerColor = Global.BLUE_PLAYER;
            }
            if (randomValue == 1)
            {
                currentPlayerColor = Global.RED_PLAYER;
            }
            if (randomValue == 2)
            {
                currentPlayerColor = Global.GREEN_PLAYER;
            }
        }
        if(currentPlayerColor == Global.BLUE_PLAYER)
        {
            skinnedMeshRenderer.material = blue;
        }
        if (currentPlayerColor == Global.RED_PLAYER)
        {
            skinnedMeshRenderer.material = red;
        }
        if (currentPlayerColor == Global.GREEN_PLAYER)
        {
            skinnedMeshRenderer.material = green;
        }
        if (currentPlayerColor == Global.YELLOW_PLAYER)
        {
            skinnedMeshRenderer.material = yellow;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentPlayerColor == Global.RED_PLAYER && other.gameObject.layer == 11)
        {
            Debug.Log("red");
           //Incrise Height
            Destroy(other.gameObject);
        }
        else if (currentPlayerColor == Global.BLUE_PLAYER && other.gameObject.layer == 13)
        {
            Vector3 obj = GameObject.Find("untitled").transform.position;
            var X=Instantiate(CFX, new Vector3(obj.x,obj.y+2,obj.z), Quaternion.identity);
            X.transform.parent = gameObject.transform;
            CFX.SetActive(true);
            Debug.Log("blue");
            if (EnergySystem.instance.currentEnergy==100)
            {
                EnergySystem.instance.currentEnergy = 100;
            }
            else
            {
                EnergySystem.instance.currentEnergy += 10;
                EnergySystem.instance.energyBar.value = EnergySystem.instance.currentEnergy;
            }
            
            //GameObject.Find("untitled").GetComponent<PoleManager>().GrowPole();
            //Increase Height
            Destroy(other.gameObject);
        }
        else if (currentPlayerColor == Global.GREEN_PLAYER && other.gameObject.layer == 12)
        {
            Debug.Log("green");
           //Increase Height
            Destroy(other.gameObject);
        }
       
        else if(other.gameObject.layer >= 11 && other.gameObject.layer <=12)
        {
            Debug.Log("wrong");
            //Decrease Height
        }
        else if (other.gameObject.tag=="HeliPoint")
        {
            FindObjectOfType<DetectShapes>().anim.SetBool("IsAttachToHeli",true);
            movement.OnHeli = true;
            
            

            //verticalVelocity += JumpSpeed;
            /*if (!isPeak)
            {
                verticalVelocity = -300 * Time.deltaTime;
                
            }*/
        }
        else if (other.gameObject.tag=="Heli")
        {
            movement.IsAttached = true; // todo 
            FindObjectOfType<DetectShapes>().anim.SetTrigger("next");
           // StartCoroutine(delayForIdle());
           
        }
        
    }

    IEnumerator delayForIdle()
    {
        yield return new WaitForSeconds(1.04f);
        FindObjectOfType<DetectShapes>().anim.SetTrigger("nextIdle");
    }
}
