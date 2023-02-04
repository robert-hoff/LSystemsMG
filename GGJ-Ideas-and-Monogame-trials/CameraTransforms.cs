using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GGJ_Ideas_and_Monogame_trials
{
    /*
     * It's not perfect, it is the world that rotates rather than calculating
     * a true orbit
     *
     */
    class CameraTransforms
    {
        public Vector3 cameraPosition = new Vector3(3f, -7f, 3f);
        public float cameraRotation = MathHelper.ToRadians(0);
        private int viewportWidth;
        private int viewportHeight;

        private float MIN_ZOOM_DISTANCE = 2f;
        private float MAX_ZOOM_DISTANCE = 30f;
        private float MIN_HEIGHT_DEGREES = -35;
        private float MAX_HEIGHT_DEGREES = 45;

        public Matrix worldMatrix { get; private set; }
        public Matrix viewMatrix { get; private set; }
        public Matrix projectionMatrix { get; private set; }

        public CameraTransforms(int viewportWidth, int viewportHeight)
        {
            this.viewportWidth = viewportWidth;
            this.viewportHeight = viewportHeight;
            CalculateWorldMatrix();
            CalculateViewMatrix();
            CalculateProjectionMatrix();
        }

        // -- World matrix and related updates
        //
        private void CalculateWorldMatrix()
        {
            worldMatrix = Matrix.CreateRotationZ(cameraRotation);
        }
        public void SetCameraOrbitDegrees(float rotateDeg)
        {
            this.cameraRotation = MathHelper.ToRadians(rotateDeg);
            CalculateWorldMatrix();
        }
        public void IncrementCameraOrbitDegrees(float rotateDeg)
        {
            this.cameraRotation += MathHelper.ToRadians(rotateDeg);
            CalculateWorldMatrix();
        }

        //
        // -- View matrix and related updates
        private void CalculateViewMatrix()
        {
            viewMatrix = Matrix.CreateLookAt(cameraPosition, new Vector3(0, 0, 0), Vector3.UnitZ);
        }
        public void ZoomIn()
        {
            if (ZoomDistance() < MIN_ZOOM_DISTANCE)
            { return; }
            cameraPosition = Vector3.Multiply(cameraPosition, 0.9f);
            CalculateViewMatrix();
        }
        public void ZoomOut()
        {
            if (ZoomDistance() > MAX_ZOOM_DISTANCE)
            { return; }
            cameraPosition = Vector3.Multiply(cameraPosition, 1.1f);
            CalculateViewMatrix();
        }
        private float ZoomDistance()
        {
            return Vector3.Distance(cameraPosition, new Vector3(0, 0, 0));
        }

        // instead of "orbit up" just raise the height (hacky)
        public void OrbitUpDown(float amount)
        {
            float cameraHeight = cameraPosition.Z + amount * ScalarProjXY(cameraPosition) / 5;
            float heightDegrees = MathHelper.ToDegrees(MathF.Atan(cameraPosition.Z / ScalarProjXY(cameraPosition)));
            if ((amount > 0 && heightDegrees > MAX_HEIGHT_DEGREES) ||
                ((amount < 0 && heightDegrees < MIN_HEIGHT_DEGREES)))
            {
                return;
            }
            cameraPosition = new Vector3(cameraPosition.X, cameraPosition.Y, cameraHeight);
            CalculateViewMatrix();
        }
        private float ScalarProjXY(Vector3 v)
        {
            return MathF.Sqrt(v.X*v.X+v.Y*v.Y);
        }

        //
        // -- Projection matrix and related updates
        private const float FOV = MathF.PI / 4; // 45 degrees
        // private const float FOV = 1.16937f; // 67 degrees was used in a previous version
        private const float NEAR_CLIP = 0.1f;
        private const float FAR_CLIP = 100f;
        private void CalculateProjectionMatrix()
        {
            float viewPortAspectRatio = (float) viewportWidth / viewportHeight;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(FOV, viewPortAspectRatio, NEAR_CLIP, FAR_CLIP);
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
