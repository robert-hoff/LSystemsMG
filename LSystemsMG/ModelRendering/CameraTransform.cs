using System;
using Microsoft.Xna.Framework;

#pragma warning disable CS0162 // Unreachable code detected
#pragma warning disable IDE0059 // Unnecessary assignment of a value
namespace LSystemsMG.ModelRendering
{
    /*
     * It's not perfect, it is the world that rotates rather than calculating
     * a true orbit
     * Is this statement correct? Is there any implications regarding direction of lighting?
     *
     */
    public class CameraTransform
    {
        // public Vector3 cameraPosition { get; private set; }  = new Vector3(3f, -7f, 3f);
        public Vector3 CameraPosition { get; private set; } = new Vector3(8f, -19f, 8f);
        public float CameraRotation { get; private set; } = MathHelper.ToRadians(-5f);
        private int viewportWidth;
        private int viewportHeight;

        private float MIN_ZOOM_DISTANCE = 8f;
        private float MAX_ZOOM_DISTANCE = 23f;
        private float MIN_HEIGHT_DEGREES = 15;
        private float MAX_HEIGHT_DEGREES = 35;

        public Matrix WorldMatrix { get; private set; }
        public Matrix ViewMatrix { get; private set; }
        public Matrix ProjectionMatrix { get; private set; }

        public CameraTransform(int viewportWidth, int viewportHeight)
        {
            this.viewportWidth = viewportWidth;
            this.viewportHeight = viewportHeight;
            CalculateWorldMatrix();
            CalculateViewMatrix();
            CalculateProjectionMatrix();
        }

        //
        // -- World matrix and related updates
        private void CalculateWorldMatrix()
        {
            WorldMatrix = Matrix.CreateRotationZ(CameraRotation);
        }
        public void SetCameraOrbitDegrees(float rotateDeg)
        {
            CameraRotation = MathHelper.ToRadians(rotateDeg / 2f);
            CalculateWorldMatrix();
        }
        public void IncrementCameraOrbitDegrees(float rotateDeg)
        {
            CameraRotation += MathHelper.ToRadians(rotateDeg / 2f);
            CalculateWorldMatrix();
        }

        //
        // -- View matrix and related updates
        private void CalculateViewMatrix()
        {
            ViewMatrix = Matrix.CreateLookAt(CameraPosition, new Vector3(0, 0, 0), Vector3.UnitZ);
        }
        public void ZoomIn()
        {
            if (Game1.RESTRICT_CAMERA && ZoomDistance() < MIN_ZOOM_DISTANCE)
            { return; }
            CameraPosition = Vector3.Multiply(CameraPosition, 0.90f);
            CalculateViewMatrix();
        }
        public void ZoomOut()
        {
            if (Game1.RESTRICT_CAMERA && ZoomDistance() > MAX_ZOOM_DISTANCE)
            { return; }
            CameraPosition = Vector3.Multiply(CameraPosition, 1.1f);
            CalculateViewMatrix();
        }
        private float ZoomDistance()
        {
            return Vector3.Distance(CameraPosition, new Vector3(0, 0, 0));
        }

        // instead of "orbit up" just raise the height (hacky)
        public void OrbitUpDown(float amount)
        {
            float cameraHeight = CameraPosition.Z + amount * ScalarProjXY(CameraPosition) / 15;

            float heightDegrees = MathHelper.ToDegrees(MathF.Atan(CameraPosition.Z / ScalarProjXY(CameraPosition)));
            if (Game1.RESTRICT_CAMERA)
            {
                if (amount > 0 && heightDegrees > MAX_HEIGHT_DEGREES)
                {
                    return;
                }
                if (amount < 0 && heightDegrees < MIN_HEIGHT_DEGREES)
                {
                    return;
                }
            }
            CameraPosition = new Vector3(CameraPosition.X, CameraPosition.Y, cameraHeight);
            CalculateViewMatrix();
        }
        private static float ScalarProjXY(Vector3 v)
        {
            return MathF.Sqrt(v.X * v.X + v.Y * v.Y);
        }

        //
        // -- Projection matrix and related updates
        private const float FOV = MathF.PI / 4; // 45 degrees
        // private const float FOV = 1.16937f; // 67 degrees was used in a previous version
        private const float NEAR_CLIP = 0.01f;
        private const float FAR_CLIP = 2400f;
        private void CalculateProjectionMatrix()
        {
            float viewPortAspectRatio = (float) viewportWidth / viewportHeight;
            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(FOV, viewPortAspectRatio, NEAR_CLIP, FAR_CLIP);
        }
        public void UpdateViewportDimensions(int viewportWidth, int viewportHeight)
        {
            if (this.viewportWidth != viewportWidth || this.viewportHeight != viewportHeight)
            {
                this.viewportWidth = viewportWidth;
                this.viewportHeight = viewportHeight;
                CalculateProjectionMatrix();
            }
        }
    }
}

