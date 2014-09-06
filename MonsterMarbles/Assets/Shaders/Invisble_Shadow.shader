Shader "Custom/Invisble_Shadow" {

    Subshader
    {
        UsePass "VertexLit/SHADOWCOLLECTOR"    
        UsePass "VertexLit/SHADOWCASTER"
    }
 
    Fallback off
}

