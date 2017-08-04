 	var Obj : Rigidbody;
	var zrotForce : int = 1;
	var MaxRot : int = 90;
	var MinRot : int = -90;
	var rotupForce : int = 1;
	var speed : float;
	var speedincrease : float;
	var speeddecrease : float;
	var Maxspeed : int;
	var Minspeed : int;
	var takeoffspeed : int;
	var lift : int;
	var minlift : int;
	var hit = false;
function Start () {

    InvokeRepeating("Speed", .1, .1);
}

function Speed(){

if (Input.GetKey(KeyCode.Space)){
Mathf.Repeat(1,Time.time);
    speed=speed+speedincrease;
    }
if (Input.GetKey(KeyCode.LeftAlt)){
Mathf.Repeat(1,Time.time);
    speed=speed-speeddecrease;
    }
}


function Update () {
var spd = Obj.velocity.magnitude;
	Obj.GetComponent.<Rigidbody>().AddRelativeForce(0,0,-speed);
    H=(Input.GetAxis ("Horizontal"))*zrotForce;
    if (H){
    Obj.GetComponent.<Rigidbody>().AddRelativeTorque(0, 0, H*(spd/100));
    }
    V=(Input.GetAxis ("Vertical"))*rotupForce;
    if (V){
    Obj.GetComponent.<Rigidbody>().AddRelativeTorque(V*(spd/100), 0, 0);
    }
    
    if(Maxspeed<=speed){
    speed=Maxspeed;
    }else{
    speed=speed;
    }
    if(Minspeed>=speed){
    speed=Minspeed;
    }else{
    speed=speed;
    }
    	if (speed<takeoffspeed){
	Obj.GetComponent.<Rigidbody>().AddForce(0,minlift,0);

	}
	if(speed>takeoffspeed){
	Obj.GetComponent.<Rigidbody>().AddForce(0,lift,0);
	}
	if (Obj.GetComponent.<Rigidbody>().rotation.z>MaxRot){
	Obj.GetComponent.<Rigidbody>().rotation.z=MaxRot;
	}
	if (Obj.GetComponent.<Rigidbody>().rotation.z<MinRot){
	Obj.GetComponent.<Rigidbody>().rotation.z=MinRot;
	}
}



