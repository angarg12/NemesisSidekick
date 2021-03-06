﻿using UnityEngine;
using System.Collections;

public class SidekickLevelController : LevelController {
	public int targetCombinedScore;

	public PlayerController hero;
	public PlayerController sidekick;

	private GameState state;

	protected new void setup(){
		base.setup ();
	}

	void Start(){
		setup ();
		mode = GameMode.Sidekick;
		state = GameState.Started;
		Time.timeScale = 1;
	}

	// Los objetivos varian segun el modo de juego. Para simplificar vamos a hacer lo siguiente.
	// En modo sidekick ambos deben sobrevivir y tener una puntuacion combinada de X.
	// En modo nemesis, debe sobrevivir solo uno de ellos, y tener una puntuacion individual de Y.
	void Update () {
		if(state == GameState.Started){
			if(hero.score + sidekick.score >= targetCombinedScore){
				LevelGUI gui = GameObject.Find("GUI").GetComponent<LevelGUI>();
				if(gui != null){
					gui.winMessageLabel = "You win!!";
					gui.win = true;
				}
				state = GameState.Win;
				disableLevel();
			}
			if(Input.GetKeyDown(KeyCode.Escape)) {
				LevelGUI gui = GameObject.Find("GUI").GetComponent<LevelGUI>();
				if(gui != null){
					gui.paused = !gui.paused;
				}
			}
		}
	}

	// Used to apply buff or anything in general.
	public override void playerKilled(PlayerController player){
		// Apply the death buff, only if it isn't game over!
		if (player.lives > 0) {
			if (hero != null && 
				player.tag == hero.tag) {
				DeathBuff.Instance.Apply (sidekick);
			} else if (sidekick != null &&
				player.tag == sidekick.tag) {
				DeathBuff.Instance.Apply (hero);
			}
		}
	}

	public override void playerEliminated(PlayerController player){
		if(state == GameState.Started){
			LevelGUI gui = GameObject.Find("GUI").GetComponent<LevelGUI>();
			if(gui != null){
				gui.lose = true;
			}
			state = GameState.Lose;
			disableLevel();
		}
	}
}
