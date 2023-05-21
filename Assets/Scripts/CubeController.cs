using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class CubeController : MonoBehaviour
{
    
   private bool canMove;
   
   private bool _front,_behind,_left,_right,_up,_down;
    public Direction _direction;
     Vector3 direction;
    Rigidbody rigidbody;
    public List<GameObject> arroundCubes;
    public List<GameObject> otherCubes;
    [Header("Side Meshes")]
    public GameObject frontMesh;
    public GameObject behindMesh;
    public GameObject leftMesh; 
    public GameObject rightMesh; 
    public GameObject upMesh;
    public GameObject downMesh;
    public bool inTrigger;
    Sequence mySequence;
    [SerializeField] GameObject hitableGameObject;
    private void Awake()
    {
        AddOtherCubes();
       
    }
    void Start()
    {
        MovedCheck();
        //for refresh
        AddOtherCubes();
        rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        RemoveHitableInList();
        
        _front =ChechForward();
       _behind=ChechBack();
       _up= ChechUp();
       _down= ChechDown();
       _right=ChechRight();
       _left=ChechLeft();
        SetDirection();
        SetTexture();
        MovedCheck();
        CheckDestroy();

    }
    #region CheckSides
    public bool ChechForward()
    {
        RaycastHit hit;

        Vector3 direction = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1) - transform.position;
      //  Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Color.red);
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
        {
            if (!arroundCubes.Contains(hit.collider.gameObject))
            {
                arroundCubes.Add(hit.collider.gameObject);
            }
            return true;
        }
        else
        {
           
            return false;
        }
        
    }

    public bool ChechBack()
    {
        RaycastHit hit;

        Vector3 direction = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1) - transform.position;
       // Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Color.red);
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
        {
            if (!arroundCubes.Contains(hit.collider.gameObject))
            {
                arroundCubes.Add(hit.collider.gameObject);
            }

            return true;
        }
        else
        {
           
            return false;
        }

    }
    public bool ChechRight()
    {
        RaycastHit hit;

        Vector3 direction = new Vector3(transform.position.x+1, transform.position.y, transform.position.z) - transform.position;
       // Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Color.red);
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
        {
            if (!arroundCubes.Contains(hit.collider.gameObject))
            {
                arroundCubes.Add(hit.collider.gameObject);
            }
            return true;
        }
        else
        {
           
            return false;
        }

    }

    public bool ChechLeft()
    {
        RaycastHit hit;

        Vector3 direction = new Vector3(transform.position.x-1, transform.position.y, transform.position.z ) - transform.position;
       // Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Color.red);
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
        {
            if (!arroundCubes.Contains(hit.collider.gameObject))
            {
                arroundCubes.Add(hit.collider.gameObject);
            }
            return true;
        }
        else
        {
           
            return false;
        }

    }
    public bool ChechUp()
    {
        RaycastHit hit;

        Vector3 direction = new Vector3(transform.position.x, transform.position.y+1, transform.position.z) - transform.position;
       // Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Color.red);
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
        {
            if (!arroundCubes.Contains(hit.collider.gameObject))
            {
                arroundCubes.Add(hit.collider.gameObject);
            }
            return true;
        }
        else
        {
            arroundCubes.Clear();
            return false;
        }

    }

    public bool ChechDown()
    {
        RaycastHit hit;

        Vector3 direction = new Vector3(transform.position.x, transform.position.y-1, transform.position.z ) - transform.position;
      //  Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Color.red);
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
        {
            if (!arroundCubes.Contains(hit.collider.gameObject))
            {
                arroundCubes.Add(hit.collider.gameObject);
            }
            return true;
        }
        else
        {
           
            return false;
        }

    }
    #endregion


    public void RemoveHitableInList()
    {
        
            if (otherCubes.Contains(hitableGameObject))
            {
                otherCubes.Remove(hitableGameObject);
            }
        
    }
    public void AddOtherCubes()
    {
        GameObject[] allCubes = GameObject.FindGameObjectsWithTag("Cube");
        for (int i = 0; i < allCubes.Length; i++)
        {
            if (allCubes[i] != gameObject)
            {
                if (!otherCubes.Contains(allCubes[i]))
                {
                    if (allCubes[i] != hitableGameObject)
                    {
                        otherCubes.Add(allCubes[i]);
                    }
                    else
                    {
                        otherCubes.Remove(allCubes[i]);
                    }

                }
                
            }
        }
    }
    public enum Direction
    {
        Right,
        Left,
        Up,
        Down,
        Forward,
        Behind
    }


  

    public void Move()
    {


        if (canMove == true)
        {

            // mySequence.Append(transform.DOMove(transform.position + direction * 10, 3f));
            OtherColliderDetectionOff();
            rigidbody.isKinematic = false;
            rigidbody.velocity = direction * 10;
            
            GameManager.Instance.minusLeftMove();
          
        }
        else
        {
            Shake();

        }
        

    }

    public void OtherColliderDetectionOff()
    {
        foreach (var cube in otherCubes)
        {
            
         Physics.IgnoreCollision(GetComponent<BoxCollider>(), cube.GetComponent<BoxCollider>(), true);
           
            
        }
    }
    
    public void MovedCheck()
    {
        
            Debug.DrawRay(transform.position, direction, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction,out hit, Mathf.Infinity))
            {
                hitableGameObject = hit.transform.gameObject;
          
        }
        
      
    }

    public void Shake()
    {
        //if canmove false cube shake code
        transform.DOShakeRotation(1, 10, 10);
    }
    public void SetDirection()
    {
        switch (_direction)
        {
            case Direction.Right:
                if (_right == true)
                {
                    canMove = false;
                
                }
                else
                {
                    direction = DirectionValue(transform.position - new Vector3(transform.position.x - 1, transform.position.y, transform.position.z));
                    canMove = true;
                }
              
                break;
            case Direction.Left:
                if (_left == true)
                {
                    canMove = false;
                   
                }
                else
                {
                    canMove = true;
                    direction = DirectionValue(transform.position - new Vector3(transform.position.x + 1, transform.position.y, transform.position.z));
                }
               
                break;
            case Direction.Up:
                if (_up == true)
                {
                    canMove = false;
                }
                else
                {
                    canMove = true;
                    direction = DirectionValue(transform.position - new Vector3(transform.position.x, transform.position.y - 1, transform.position.z));
                }

                break;
            case Direction.Down:
                if(_down== true)
                {
                    canMove = false;
                }
                else
                {
                    canMove = true;
                    direction = DirectionValue(transform.position - new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
                }
                break;
            case Direction.Forward:
                if (_front == true)
                {
                    canMove = false;
                }
                else
                {
                    canMove= true;
                    direction = DirectionValue(transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z - 1));
                }
                break;
            case Direction.Behind:
               if(_behind == true)
                {
                    canMove = false;
                }
                else
                {
                    canMove = true;
                    direction = DirectionValue(transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z + 1));
                }
                break;

        }
    }

    public Vector3 DirectionValue(Vector3 value)
    {
        return value;
    }

    public void SetTexture()
    {
        switch (_direction)
        {
            case Direction.Right:
                rightMesh.SetActive(false);
                leftMesh.SetActive(false);
                upMesh.SetActive(true);
                downMesh.SetActive(true);
                behindMesh.SetActive(true);
                frontMesh.SetActive(true);
                //Ok y�n�ne D�nd�rme Kodlar� Buraya Yaz�lacak
                upMesh.transform.localRotation = Quaternion.Euler(90,180, 90);
                downMesh.transform.localRotation = Quaternion.Euler(-90, -180, 90);
                behindMesh.transform.rotation = Quaternion.Euler(0,0, - 90);
                frontMesh.transform.rotation = Quaternion.Euler(0,180,90);
                break;
            case Direction.Left:
                rightMesh.SetActive(false);
                leftMesh.SetActive(false);
                upMesh.SetActive(true);
                downMesh.SetActive(true);
                behindMesh.SetActive(true);
                frontMesh.SetActive(true);
                //Ok y�n�ne D�nd�rme Kodlar� Buraya Yaz�lacak
                upMesh.transform.localRotation = Quaternion.Euler(90, 180, -90);
                downMesh.transform.localRotation = Quaternion.Euler(-90, -180, -90);
                behindMesh.transform.rotation = Quaternion.Euler(0, 0, 90);
                frontMesh.transform.rotation = Quaternion.Euler(0, 180, -90);
                break;
            case Direction.Up:
                rightMesh.SetActive(true);
                leftMesh.SetActive(true);
                upMesh.SetActive(false);
                downMesh.SetActive(false);
                behindMesh.SetActive(true);
                frontMesh.SetActive(true);
                //Ok y�n�ne D�nd�rme Kodlar� Buraya Yaz�lacak
                rightMesh.transform.localRotation = Quaternion.Euler(0, -90, -0);
                leftMesh.transform.localRotation = Quaternion.Euler(0, 90, 0);
                behindMesh.transform.rotation = Quaternion.Euler(0, 0, 0);
                frontMesh.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case Direction.Down:
                rightMesh.SetActive(true);
                leftMesh.SetActive(true);
                upMesh.SetActive(false);
                downMesh.SetActive(false);
                behindMesh.SetActive(true);
                frontMesh.SetActive(true);
                //Ok y�n�ne D�nd�rme Kodlar� Buraya Yaz�lacak
                rightMesh.transform.localRotation = Quaternion.Euler(0, -90, 180);
                leftMesh.transform.localRotation = Quaternion.Euler(0, 90, 180);
                behindMesh.transform.rotation = Quaternion.Euler(0, 0, 180);
                frontMesh.transform.rotation = Quaternion.Euler(0, 180, 180);
                break;
            case Direction.Forward:
                rightMesh.SetActive(true);
                leftMesh.SetActive(true);
                upMesh.SetActive(true);
                downMesh.SetActive(true);
                behindMesh.SetActive(false);
                frontMesh.SetActive(false);
                //Ok y�n�ne D�nd�rme Kodlar� Buraya Yaz�lacak
                rightMesh.transform.localRotation = Quaternion.Euler(0, -90, -90);
                leftMesh.transform.localRotation = Quaternion.Euler(0, 90, 90);
                upMesh.transform.rotation = Quaternion.Euler(90, 180, 180);
                downMesh.transform.rotation = Quaternion.Euler(-90, 180, 0);
                break;
            case Direction.Behind:
                rightMesh.SetActive(true);
                leftMesh.SetActive(true);
                upMesh.SetActive(true);
                downMesh.SetActive(true);
                behindMesh.SetActive(false);
                frontMesh.SetActive(false);
                //Ok y�n�ne D�nd�rme Kodlar� Buraya Yaz�lacak
                rightMesh.transform.localRotation = Quaternion.Euler(0, -90, 90);
                leftMesh.transform.localRotation = Quaternion.Euler(0, 90, -90);
                upMesh.transform.rotation = Quaternion.Euler(90, 180, 0);
                downMesh.transform.rotation = Quaternion.Euler(-90, 180, 180);
                break;
          
        }
    }

    public void CheckDestroy()
    {
        if (inTrigger == false)
        {
            foreach (var cube in otherCubes)
            {
                cube.GetComponent<CubeController>().otherCubes.Remove(cube);
            }
            gameObject.SetActive(false);
            GameManager.Instance.RemoveLeftCube(this.gameObject);
        }
       
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == hitableGameObject)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.isKinematic = true;
            Shake();


        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        inTrigger=true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }
}