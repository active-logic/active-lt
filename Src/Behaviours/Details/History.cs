using UnityEngine;
using System.Collections.Generic;
using NGIEnumerator = System.Collections.IEnumerator;
using NGIEnumerable = System.Collections.IEnumerable;

namespace Active.Core.Details{
public class History : IEnumerable<VTrace>{

	List<VTrace> keys = new List<VTrace>();

	VTrace previous => keys.Count > 0 ? keys[keys.Count-1] : null;

	public VTrace this[int n]{ get{
		foreach(var x in keys) if(x.frame == n) return x;
		return null;
	}}

	public IEnumerator<VTrace> GetEnumerator() => keys.GetEnumerator();

	public void Log(string log, Transform src){
		log = TrimLog(log);
		if(previous != null && log == previous.log) return;
		var x = new VTrace();
		x.frame    = Time.frameCount;
		x.log 	   = log;
		x.position = src.position;
		x.rotation = src.rotation;
		x.previous = previous;
		keys.Add(x);
	}

	public VTrace Previous(int n){
		VTrace prev = null;
		foreach(var x in keys){ if(x.frame == n) return prev; prev = x; }
		return prev?.frame < n ? prev : null;
	}

	public VTrace Next(int n){
		VTrace prev = null;
		foreach(var x in keys){ if(prev?.frame == n) return x; prev = x; }
		return null;
	}

	public static string TrimLog(string s)
	=> s == null ? s : TrimDetails(s, '[', ']');

	NGIEnumerator NGIEnumerable.GetEnumerator() => GetEnumerator();

	static string TrimDetails(string self, char @in, char @out){
		var x = new System.Text.StringBuilder();
		var depth = 0;
		for(int i = 0; i < self.Length; i++){
			var c = self[i];
			if(c == @in) depth++;
			if(depth <= 0) x.Append(c);
			if(c==@out) depth--;
		} return x.ToString();
	}

}}
