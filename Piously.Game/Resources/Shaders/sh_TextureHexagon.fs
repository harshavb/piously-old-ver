#include <math.h>
#include "sh_Utils.h"

#define SIN_PI_OVER_3 0.8660254037844386
#define TAN_PI_OVER_3 1.732050807568877

varying mediump vec2 v_TexCoord;

uniform lowp sampler2D m_Sampler;

// this is the rotation of the hexagonal container AND the "size" of the object in screen space--specifically max(width, height).
uniform highp vec2 g_RotationAndResolution;

// equation 1 is the line on top
highp float equ1(mediump vec2 coord, highp float rotation)
{
    return coord.y - SIN_PI_OVER_3;
}

// equation 2 is the top right line
highp float equ2(mediump vec2 coord, highp float rotation)
{
    return TAN_PI_OVER_3 * coord.x + coord.y - 2.0 * SIN_PI_OVER_3;
}

// equation 3 is the top left line
highp float equ3(mediump vec2 coord, highp float rotation)
{
    return -TAN_PI_OVER_3 * coord.x + coord.y - 2.0 * SIN_PI_OVER_3;
}

// equation 4 is the line on bottom
highp float equ4(mediump vec2 coord, highp float rotation)
{
    return -coord.y - SIN_PI_OVER_3;
}

// equation 5 is the bottom left line
highp float equ5(mediump vec2 coord, highp float rotation)
{
    return TAN_PI_OVER_3 * coord.x - coord.y - 2.0 * SIN_PI_OVER_3;
}

// equation 6 is the bottom right line
highp float equ6(mediump vec2 coord, highp float rotation)
{
    return -TAN_PI_OVER_3 * coord.x - coord.y - 2.0 * SIN_PI_OVER_3;
}

// a visual demonstration of this calculation can be found at https://www.desmos.com/calculator/vihhpowcrb.
highp float calculateHexagonDistance(mediump vec2 coord, highp vec2 g_RotationAndResolution)
{
    mediump vec2 tmp = (coord - vec2(0.5));
    mediump vec2 norm = vec2(tmp.x * 2.0, tmp.y * 2.0);

    // If these values are all less than zero, it is in the hexagon
    highp float y1 = equ1(norm, g_RotationAndResolution.x);
    highp float y2 = equ2(norm, g_RotationAndResolution);
    highp float y3 = equ3(norm, g_RotationAndResolution);
    highp float y4 = equ4(norm, g_RotationAndResolution);
    highp float y5 = equ5(norm, g_RotationAndResolution);
    highp float y6 = equ6(norm, g_RotationAndResolution);

    if(y1 <= 0.0 && y2 <= 0.0 && y3 <= 0.0 && y4 <= 0.0 && y5 <= 0.0 && y6 <= 0.0) {
        return 1.0;
    } else {
        return 0.0;
    }
    
}

void main(void)
{
    gl_FragColor = toSRGB(texture2D(m_Sampler, v_TexCoord));

    // distance in pixels from the edge of the hexagon (assuming a square draw quad >_>).
    highp float distance = calculateHexagonDistance(v_TexCoord, g_RotationAndResolution) * g_RotationAndResolution.y / 2.0;

    if (distance < 0.5)
    {
        gl_FragColor.a = 0.0;
    }
    else
    {
        gl_FragColor.a = 1.0;
    }
}