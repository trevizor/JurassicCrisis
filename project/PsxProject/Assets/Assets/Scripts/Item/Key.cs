using UnityEngine;
using System.Collections;

public class Key : Item {

	public string fingerprint { get; set; }

    
    public Key (string _fingerprint, string _title, string _description = "", string _mesh = "", string _icon = "") 
    : base(_title, _description, _mesh, _icon) {
        
        fingerprint = _fingerprint;
	}

	public string GetFingerPrint () {
		return fingerprint;
	}

}
