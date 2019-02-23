using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPAnchorCtrl : WPUIManager {

	public static WPAnchorCtrl instance=null;

	protected override void Init()
	{
		instance = this;

		base.SetActive(false);
	}
	
}
