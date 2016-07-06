using UnityEngine;
using System.Collections;

public abstract class Item {

	public string title { get; set; }
	public string description { get; set; }
	public string mesh { get; set; }
	public string icon { get; set; }

	public Item (string _title = "", string _description = "", string _mesh = "", string _icon = "") {
        title = _title;
        description = _description;
		mesh = _mesh;
		icon = _icon;
	}
}
