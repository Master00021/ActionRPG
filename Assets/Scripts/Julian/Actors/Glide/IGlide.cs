using UnityEngine;

public interface IGlide {
    
    public void Impulse(GlideConfiguration configuration, Rigidbody rigidbody, Transform finalPosition);
    public void Glide(Rigidbody rigidbody);
}
