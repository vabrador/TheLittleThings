#pragma strict

function Start () {

}
var target : Transform;
var distance : float;
function Update () {
	transform.position.z = target.position.z - distance;
	transform.position.y = target.position.y;
	transform.position.x = target.position.x;
}