/*
	Author: Mark Toledo
	Description: Used to call any state from the menuStateMachine
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YeBoisFramework.StateMachine;
namespace YeBoisFramework.UI
{
	public class MenuState : State 
	{
		#region Properties and Fields
		public MenuStateName menuName;
		public override string stateName { get { return menuName.ToString(); } }

		private MenuStateMachine menuStateMachine;
		#endregion

		#region Unity Methods
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			menuStateMachine = animator.gameObject.GetComponent<MenuStateMachine>();
			if(menuStateMachine == null) { Debug.LogError("MenuStateMachine Does Not Exist"); }
			else { menuStateMachine.RefreshStateMachine((int) menuName); }
		}
		#endregion
	}
	public enum MenuStateName
	{
		Start,
		Settings,
		Game
	}
}
