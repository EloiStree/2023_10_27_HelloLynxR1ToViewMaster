#ifndef YUVRGB
#define YUVRGB


#define Y_THIRD 1.0 / 3.0

float4 YUV_ST;


float4 YUV2RGB(float2 uvCoord, sampler2D YUV, float2 resolution)
{
    //--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y--y
    float2 yUVC = uvCoord;
    yUVC.y /= 1.5;
    float y = tex2D(YUV, yUVC).x;


    //--uv--uv--uv--uv--uv--uv--uv--uv--uv--uv--uv--uv--uv--uv--uv--uv--uv--uv--uv--uv
    float UVYC = (uvCoord.y * Y_THIRD) + 2.0 * Y_THIRD;
    float UVXCBase = (uvCoord.x * 0.5) * resolution.x;

    //--u--u--u--u
    float UVXC = (UVXCBase + floor(UVXCBase.x)) / resolution;
    float u = tex2D(YUV, float2(UVXC, UVYC)).x;
    //--v--v--v--v
    float VUVCx = UVXC + (1.0 / resolution);
    float v = tex2D(YUV, float2(VUVCx, UVYC)).x;

    //--conversion--conversion--conversion--conversion--conversion--conversion--conversion--conversion
    int nY = (int)(floor(y * 256.0) - 16.0);
    int nU = (int)(floor(u * 256.0) - 128.0);
    int nV = (int)(floor(v * 256.0) - 128.0);

    int nR = (int)(1192 * nY + 1634 * nV);
    int nG = (int)(1192 * nY - 833 * nV - 400 * nU);
    int nB = (int)(1192 * nY + 2066 * nU);

    nR = min(262143, max(0, nR));
    nG = min(262143, max(0, nG));
    nB = min(262143, max(0, nB));

    nR = (nR >> 10) & 0xff;
    nG = (nG >> 10) & 0xff;
    nB = (nB >> 10) & 0xff;

    return float4(
        (float)nR / 256.0,
        (float)nG / 256.0,
        (float)nB / 256.0,
        1.0);
}
#endif