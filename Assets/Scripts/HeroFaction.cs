﻿using UnityEngine;
using System.Collections;

public class HeroFaction : Faction {
	public override void destroy(GameObject orderer){
		if(orderer.tag == "enemy"){
			Destroy(gameObject);
		}
	}
}
