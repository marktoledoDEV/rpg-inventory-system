/*
	Author: Mark Toledo
	Description: Keeps states of its type consistent and in line
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YeBoisFramework.StateMachine
{
	[RequireComponent(typeof(Animator))]
	public abstract class BaseStateMachine : MonoBehaviour
	{
		#region Components
		private Animator smAnimator;
		#endregion

		#region Unity Methods
		protected virtual void Awake()
		{
			smAnimator = GetComponent<Animator>();
			InitializeStates();
		}
		#endregion

		#region Custom Methods
		//Will bind any states from the animator together to make it easily accessible to the state machine
		protected abstract void InitializeStates();
		//Will be called in a statemachine controller to refresh the statemachine
		public abstract void RefreshStateMachine(int trigger);
		#endregion
	}
}
