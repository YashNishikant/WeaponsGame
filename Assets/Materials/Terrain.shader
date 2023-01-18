Shader "Custom/Terrain"
{
    Properties{
        _AngleFloat ("steep1", Float) = 0
        _AngleFloat2 ("steep2", Float) = 0
        _AngleFloat3 ("steep3", Float) = 0
        _AngleFloat4 ("steep4", Float) = 0
    }
        SubShader{
          CGPROGRAM
          #pragma surface surf Lambert vertex:vert

          struct Input {
              float3 customColor;
              float3 worldNormal;
              float3 worldPos;
          };

        void vert(inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o)

        }
        float _AngleFloat;
        float _AngleFloat2;
        float _AngleFloat3;
        float _AngleFloat4;

        void surf(Input IN, inout SurfaceOutput o) {

                
              float3 worldNormal = WorldNormalVector(IN, o.Normal);
              half w = dot(worldNormal, normalize(half3(0, 1, 0)));
              half h = IN.worldPos.y;

              //flat grass
              if (abs(w) < _AngleFloat4) { 
                  IN.customColor = float3(0.25, 0.35, 0.09) * (abs(w)*2);
              }
              //hill brown
              if (abs(w) < _AngleFloat3) { 
                    IN.customColor = float3(0.53, 0.41, 0);
              }
              //mountain gray
              if (abs(w) < _AngleFloat2) { 
                  IN.customColor = float3(0.46, 0.46, 0.46);
              }

              if (abs(w) < 0) {
                  IN.customColor = float3(0.46, 0.46, 0.46);
              }

              //snow and sand
              if (h < 10) {
                  IN.customColor = float3(0.99, 0.86, 0.57);
              }

              if (h > 88 && abs(w) > 0.5) {
                  IN.customColor = float3(1, 1, 1);
              }

              o.Albedo = IN.customColor;
        }
        ENDCG
    }
        Fallback "Diffuse"
}