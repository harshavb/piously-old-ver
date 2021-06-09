#include <math.h>
#include "sh_Utils.h"

#define PI              3.1415926535897932
#define SIN_PI_OVER_3   0.8660254037844386
#define TAN_PI_OVER_3   1.732050807568877

varying mediump vec2 v_TexCoord;

uniform lowp sampler2D m_Sampler;

// this is the rotation of the hexagonal container AND the "size" of the object in screen space--specifically max(width, height).
// g_RotationAndResolution.x = rotation
// g_RotationAndResolution.y = resolution
uniform highp vec2 g_RotationAndResolution;

// a visual demonstration of this calculation can be found at https://www.desmos.com/calculator/vihhpowcrb.
highp float calculateHexagonDistance(mediump vec2 coord, highp vec2 g_RotationAndResolution)
{
    // recall from above the definition of g_RotationAndResolution
    highp float rotation = g_RotationAndResolution.x;

    // this converts the coord vector to a "normal" vector relative to the position and size of the hex
    // "coord" is relative to the top left (range 0 - 1)
    // "norm" is relative to the center (range -1 - 1, but absolute value cause it's difference)
    mediump vec2 tmp = (coord - vec2(0.5));
    mediump vec2 norm_not_rotated = vec2(abs(tmp.x * 2.0), abs(tmp.y * 2.0));
    mediump float dist = sqrt(norm_not_rotated.x * norm_not_rotated.x + norm_not_rotated.y * norm_not_rotated.y);
    mediump float angle;
    if(norm_not_rotated.x != 0.0) {
        angle = atan(norm_not_rotated.y / norm_not_rotated.x);
    } else {
        angle = PI/2.0;
    }
    mediump vec2 norm = vec2(dist * cos(angle + (rotation * PI/180.0)), dist * sin(angle + (rotation * PI/180.0)));

    // d1 is the distance from coord to the right bound.
    highp float d1 = (norm.x * -TAN_PI_OVER_3 - norm.y + TAN_PI_OVER_3) / 2.0;
    // d2 is the distance from coord to the top bound.
    highp float d2 = (SIN_PI_OVER_3 - norm.y);

    return min(d1, d2);
}

void main(void)
{
    gl_FragColor = toSRGB(texture2D(m_Sampler, v_TexCoord));

    // distance in pixels from the edge of the hexagon (assuming a square draw quad >_>).
    highp float distance = calculateHexagonDistance(v_TexCoord, g_RotationAndResolution) * g_RotationAndResolution.y / 2.0;

    if (distance <= 0.0)
    {
        gl_FragColor.a = 0.0;
    }
    else
    {
        // blending range is implicitly 1.0.
        gl_FragColor.a *= distance;
    }
}