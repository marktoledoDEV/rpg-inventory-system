/*
	Author: Mark Toledo
	Description: An class that establishes the functions that a state could do in a Animator
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YeBoisFramework.StateMachine
{
	public abstract class State : StateMachineBehaviour 
	{
		#region Properties and Fields
		public virtual string stateName { get; set; }
		#endregion

		#region Unity Methods
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
		{
		}
		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}
		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}
		#endregion
	}
}
