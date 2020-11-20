using UnityEngine;

namespace Active.Core.Details{
public class UGigAdapter : UGig{

    public System.Func<status> call;
    public UGig next;

    override public status Step(){
        var s = call();
        if(next != null && s.complete){
            GetComponent<Stepper>().root = next;
            Destroy(this);
        }
        return s;
    }

}}
