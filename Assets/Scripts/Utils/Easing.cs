using UnityEngine;

/// <summary>
/// Commonly used easing functions. Check https://easings.net/ for reference.
/// </summary>
public static class Easing {
        public delegate float EasingFunc(float t);

        // ---- Sine ---- //
        public static float InSine(float t) {
            return 1 - Mathf.Cos(t * Mathf.PI / 2);
        }

        public static float OutSine(float t) {
            return Mathf.Sin((t * Mathf.PI) / 2);
        }

        public static float InOutSine(float t) {
            return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
        }

        // ---- Quad ---- //
        public static float InQuad(float t) {
            return t * t;
        }

        public static float OutQuad(float t) {
            return 1 - (1 - t) * (1 - t);
        }

        public static float InOutQuad(float t) {
            return t < 0.5 ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
        }

        // ---- Cubic ---- //
        public static float InCubic(float t) {
            return t * t * t;
        }

        public static float OutCubic(float t) {
            return 1 - Mathf.Pow(1 - t, 3);
        }

        public static float InOutCubic(float t) {
            return t < 0.5 ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
        }

        // ---- Quart ---- //
        public static float InQuart(float t) {
            return t * t * t * t;
        }

        public static float OutQuart(float t) {
            return 1 - Mathf.Pow(1 - t, 4);
        }

        public static float InOutQuart(float t) {
            return t < 0.5 ? 8 * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 4) / 2;
        }

        // ---- Quint ---- //
        public static float InOutQuint(float t) {
            return t < 0.5 ? 16 * t * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 5) / 2;
        }

        // ---- Expo ---- //
        public static float InExpo(float t) {
            return t == 0 ? 0 : Mathf.Pow(2, 10 * t - 10);
        }

        public static float OutExpo(float t) {
            return t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
        }

        // ---- Circ ---- //
        public static float InCirc(float t) {
            return 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));
        }

        // ---- Bounce ---- //
        public static float OutBounce(float t) {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (t < 1 / d1) {
                return n1 * t * t;
            } else if (t < 2 / d1) {
                return n1 * (t -= 1.5f / d1) * t + 0.75f;
            } else if (t < 2.5 / d1) {
                return n1 * (t -= 2.25f / d1) * t + 0.9375f;
            } else {
                return n1 * (t -= 2.625f / d1) * t + 0.984375f;
            }
        }

        public static float InOutBounce(float t) {
            return t < 0.5
                ? (1 - OutBounce(1 - 2 * t)) / 2
                : (1 + OutBounce(2 * t - 1)) / 2;
        }

        // ---- Elastic ---- //
        public static float OutElastic(float t) {
            const float c4 = (2 * Mathf.PI) / 3;

            return t == 0
              ? 0
              : t == 1
                ? 1
                : Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * c4) + 1;
        }

        // ---- Other ---- //
        public static float None(float t) => t;

        private static float AssymetricEasing(float t, float b) {
            // Base function: f(t) = C.t^a.exp(-b.t)
            // a controls how quickly it rises
            // b controls how quickly it falls
            // Peaks when x = a/b
            // C is there to guarantee a peak at 1 when x = 1

            float C = b / Mathf.Exp(-1);
            return C * t * Mathf.Exp(-b * t);
        }

        public static float AssymetricInOut(float t) {
            return AssymetricEasing(t, 8);
        }
    }