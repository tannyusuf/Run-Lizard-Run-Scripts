using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the camera that follows the player character
/// Allows for orbit and zoom functionality
/// </summary>
public class IguanaCameraScript : MonoBehaviour {
    // Target and camera references
    public GameObject target;           // Object to follow (player)
    public float turnSpeed=.2f;         // Camera rotation speed
    public GameObject iguanaCamera;     // Camera game object
    
    // Camera angle variables
    float cameraAngleX=180f;            // Vertical camera angle
    float cameraAngleY=0f;              // Horizontal camera angle
    public float cameraDistance=3f;     // Distance from target
    
    /// <summary>
    /// Initialize camera position and rotation
    /// </summary>
    public void Start(){
        // Set initial camera rotation
        Quaternion arotation = Quaternion.identity;
        Vector3 eua = Vector3.zero;
        eua.y = 360f-cameraAngleY;
        eua.z = 0f;
        eua.x = 180f+cameraAngleX;
        arotation.eulerAngles = eua;
        transform.localRotation= arotation;
    }
    
    /// <summary>
    /// Handle camera controls each frame
    /// </summary>
    void Update(){
        // Rotate camera when right mouse button is held
        if (Input.GetKey (KeyCode.Mouse1)) {
            cameraAngleY+= Input.GetAxis("Mouse X");
            cameraAngleX+= Input.GetAxis("Mouse Y");
        }
        
        // Apply camera rotation
        CameraRotationX ();
        CameraRotationY ();
        
        // Adjust distance with mouse wheel
        cameraDistance=cameraDistance+Input.GetAxis ("Mouse ScrollWheel");
        
        // Position camera based on distance
        iguanaCamera.transform.localPosition = new Vector3 (0f,cameraDistance,-2f*cameraDistance);
    }
    
    /// <summary>
    /// Set a new target for the camera to follow
    /// </summary>
    /// <param name="aTarget">New target object</param>
    public void TargetSet(GameObject aTarget){
        target = aTarget;
    }
    
    /// <summary>
    /// Apply X-axis rotation to camera
    /// </summary>
    public void CameraRotationX(){
        Quaternion arotation = Quaternion.identity;
        Vector3 eua = Vector3.zero;
        eua.y = 360f-cameraAngleY;
        eua.z = 0f;
        eua.x = 180f+cameraAngleX;
        arotation.eulerAngles = eua;
        transform.localRotation= arotation;
    }
    
    /// <summary>
    /// Apply Y-axis rotation to camera
    /// </summary>
    public void CameraRotationY(){
        Quaternion arotation = Quaternion.identity;
        Vector3 eua = Vector3.zero;
        eua.y = 360f-cameraAngleY;
        eua.z = 0f;
        eua.x = 180f+cameraAngleX;
        arotation.eulerAngles = eua;
        transform.localRotation= arotation;
    }
    
    /// <summary>
    /// Smoothly move camera to target position
    /// </summary>
    void FixedUpdate(){
        transform.position = Vector3.Lerp (transform.position,target.transform.position,Time.deltaTime*10f);
    }
}
