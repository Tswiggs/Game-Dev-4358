using UnityEngine;
using System.Collections;

public class ToggleButtonController : MonoBehaviour {

	private Animator _animator;
	public int index;
	
	public bool IsToggled {
		get { return _animator.GetBool("IsToggled");}
		set { _animator.SetBool ("IsToggled", value);}
	}
	
	public void Awake(){
		_animator = GetComponent<Animator>();
	}
	
	public void toggle() {
		_animator.SetBool ("IsToggled",!_animator.GetBool("IsToggled"));
	}
}
