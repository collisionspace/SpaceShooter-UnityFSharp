namespace SpaceShooter
open UnityEngine


type PlayerController() =
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable speed = 6.0f
    [<SerializeField>]
    let mutable tilt = 0.0f
    [<SerializeField>]
    let mutable xMin = 0.0f
    [<SerializeField>]
    let mutable xMax = 0.0f
    [<SerializeField>]
    let mutable zMin = 0.0f
    [<SerializeField>]
    let mutable zMax = 0.0f

    [<SerializeField>]
    let mutable shot = Unchecked.defaultof<GameObject>    
    [<SerializeField>]
    let mutable shotSpawn = Unchecked.defaultof<Transform>
    [<SerializeField>]
    let mutable fireRate = 0.25f
   
    let mutable nextFire = 0.0f
    let mutable clone = Unchecked.defaultof<Object>

    (*This member is called every cycle. Tried to use pattern match here instead of if statement but kept 
    getting an error. This adds a wait time to firing, also creates a new shot object, and plays a audio clip*)
    member x.Update() =
        if Input.GetButton("Fire1") && Time.time > nextFire then
            nextFire <- Time.time + fireRate
            clone <- Object.Instantiate(shot,shotSpawn.position,shotSpawn.rotation)
            x.GetComponent<AudioSource>().Play()

    member x.FixedUpdate () =
        //grabs input for vertical and horizontal
        let ``move horizontal`` = Input.GetAxis("Horizontal");
        let ``move vertical`` = Input.GetAxis("Vertical")

        //creates movement based on the input from the user
        let movement = Vector3(``move horizontal``, 0.0f, ``move vertical``)

        //serts the velocity on the said object
        x.GetComponent<Rigidbody>().velocity <- movement * speed
        
        //changes the position within the given boundary
        x.GetComponent<Rigidbody>().position <- Vector3(
            Mathf.Clamp(x.GetComponent<Rigidbody>().position.x,xMin,xMax),  //x
            0.0f,   //y
            Mathf.Clamp( x.GetComponent<Rigidbody>().position.z,zMin,zMax))  //z
        
        //tilts the ships to the left or to the right
        x.GetComponent<Rigidbody>().rotation <- Quaternion.Euler(0.0f,0.0f,x.GetComponent<Rigidbody>().velocity.x * -tilt)