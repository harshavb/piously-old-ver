#include "sh_Utils.h"

#define SIN_PI_OVER_3 0.8660254037844386
#define TAN_PI_OVER_3 1.732050807568877

varying mediump vec2 v_TexCoord;

uniform lowp sampler2D m_Sampler;

bool withinHexagon(mediump vec2 coord)
{
    mediump vec2 norm = (coord - vec2(0.5)) * 2;

    if (abs(norm.y) > SIN_PI_OVER_3) {
        return false; // top and bottom bounds
    }

    if (abs(norm.y) > -TAN_PI_OVER_3 * (norm.x - 1)) {
        return false; // right bounds
    }

    if (abs(norm.y) > TAN_PI_OVER_3 * (norm.x + 1)) {
        return false; // right bounds
    }

    return true;
}

void main(void)
{
    gl_FragColor = toSRGB(texture2D(m_Sampler, v_TexCoord));

    if (!withinHexagon(v_TexCoord)) {
        gl_FragColor.a = 0;
    }
}