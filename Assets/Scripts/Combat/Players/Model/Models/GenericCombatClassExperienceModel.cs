using UnityEngine;
using System.Collections;

/**
 * Represent a generic class.
 * 
 * Notes:
 * 	XP: represented by the equation xp=13*level^3-13
 * 	Level: represented by the equation cuberoot(13+xp/13)=level
 * 
 * Intended XP Table:
 * <table>
 * <th><td>Level</td> <td>XP</td></th>
 *	<tr><td>1</td> <td>0</td></tr>
 *	<tr><td>2</td> <td>91</td></tr>
 *	<tr><td>3</td> <td>338</td></tr>
 *	<tr><td>4</td> <td>819</td></tr>
 *	<tr><td>5</td> <td>1612</td></tr>
 *	<tr><td>6</td> <td>2795</td></tr>
 *	<tr><td>7</td> <td>4446</td></tr>
 *	<tr><td>8</td> <td>6643</td></tr>
 *	<tr><td>9</td> <td>9464</td></tr>
 *	<tr><td>10</td> <td>12987</td></tr>
 *	<tr><td>11</td> <td>17290</td></tr>
 *	<tr><td>12</td> <td>22451</td></tr>
 *	<tr><td>13</td> <td>28548</td></tr>
 *	<tr><td>14</td> <td>35659</td></tr>
 *	<tr><td>15</td> <td>43862</td></tr>
 *	<tr><td>16</td> <td>53235</td></tr>
 *	<tr><td>17</td> <td>63856</td></tr>
 *	<tr><td>18</td> <td>75803</td></tr>
 *	<tr><td>19</td> <td>89154</td></tr>
 *	<tr><td>20</td> <td>103987</td></tr>
 *	<tr><td>21</td> <td>120380</td></tr>
 *	<tr><td>22</td> <td>138411</td></tr>
 *	<tr><td>23</td> <td>158158</td></tr>
 *	<tr><td>24</td> <td>179699</td></tr>
 *	<tr><td>25</td> <td>203112</td></tr>
 *	<tr><td>26</td> <td>228475</td></tr>
 *	<tr><td>27</td> <td>255866</td></tr>
 *	<tr><td>28</td> <td>285363</td></tr>
 *	<tr><td>29</td> <td>317044</td></tr>
 *	<tr><td>30</td> <td>350987</td></tr>
 *	<tr><td>31</td> <td>387270</td></tr>
 *	<tr><td>32</td> <td>425971</td></tr>
 *	<tr><td>33</td> <td>467168</td></tr>
 *	<tr><td>34</td> <td>510939</td></tr>
 *	<tr><td>35</td> <td>557362</td></tr>
 *	</table>
 *
 */
public class GenericCombatClassExperienceModel : AbstractCombatClassModel {
	public int level;
	
	public GenericCombatClassExperienceModel() {
		level = 0;
	}
	
	#region ICombatClass implementation
	public override void SetLevelByXp(int experience) {
		level = GetLevelForExp(experience);
	}
	
	public override int GetExpNeededForLevel(int level) {
		return 13*(level^3)-13;
	}
	
	public override int GetLevelForExp(int exp) {
		return (int)System.Math.Floor(System.Math.Pow((13+exp/13), 1.0/3.0));
	}
	#endregion
	
	#region IClassModel implementation
	public override bool AddExperience(int experience) {
		//does nothing
		return false;
	}

	public override int Level {
		get {
			return level;
		}
	}

	public override int Experience {
		get {
			return 0;
		}
	}
	#endregion

	#region ICombat implementation
	public override int Hits {
		get {
			return 30 + (level * 5);
		}
		set {
			
		}
	}

	public override int Energy {
		get {
			return 6 + (level * 3);
		}
		set {
			
		}
	}

	public override int Attack {
		get {
			return 10 + (level * 4);
		}
		set {
			
		}
	}

	public override int Defense {
		get {
			return 8 + (level * 3);
		}
		set {
			
		}
	}

	public override int Special {
		get {
			return 5 + (level * 2);
		}
		set {
			
		}
	}
	
	public override float Speed {
		get {
			return (float)(1.0 + (0.15 * level));
		}
		set {
			
		}
	}
	#endregion
}
