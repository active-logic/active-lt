using UnityEngine;

namespace Active.Core.Details{
public class VTrace {

	public string     log;
	public float      span, stamp;
	public int        frame = -1;
	public VTrace     previous;
	public Vector3    position;
	public Quaternion rotation;

	public VTrace(){ stamp = Time.time; }

	bool isOutOfDate => Time.time - stamp > span;

}}
